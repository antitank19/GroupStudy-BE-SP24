using API.SignalRHub.Tracker;
using APIExtension.ClaimsPrinciple;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using DataLayer.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using ShareResource.DTO.Connection;
using System.Net.Http;
using static API.SignalRHub.DrawHub;

namespace API.SignalRHub
{
    [Authorize]
    public class MeetingHub : Hub
    {
        #region Message
        //Thông báo có người mới vào meeting
        //BE SendAsync(UserOnlineInGroupMsg, MemberSignalrDto)
        public static string UserOnlineInMeetingMsg => "UserOnlineInMeeting";

        //Dùng để test coi có connect thành công chưa. Nếu connect thành công, be sẽ
        //gửi 1 đoạn msg thông báo là đã connect meetingHub thành công
        //BE SendAsync(OnConnectMeetHubSuccessfullyMsg, msg: string);
        public static string OnConnectMeetHubSuccessfullyMsg => "OnConnectMeetHubSuccessfully";
        //Dùng để test coi có invoke thành công không. Nếu invoke thành công, be sẻ 
        //gửi 1 đoạn msg thông báo đã invoke thành công
        public static string OnTestReceiveInvokeMsg => "OnTestReceiveInvoke";

        //Thông báo có người rời meeting
        //BE SendAsync(UserOfflineInGroupMsg, offlineUser: MemberSignalrDto)
        public static string UserOfflineInMeetingMsg => "UserOfflineInMeeting";

        //Thông báo có user nào đang show screen ko. Cho FE biết để chuyển  
        //màn hình chính qua lại chế độ show các cam và chế độ share screen
        //BE SendAsync(OnShareScreenMsg, isShareScreen: bool)
        public static string OnShareScreenMsg => "OnShareScreen";

        //Thông báo tới người đang share screen là có người mới, shareScreenPeer share luôn cho người này
        //BE SendAsync(OnShareScreenLastUser, new { usernameTo: string, isShare: bool })
        public static string OnShareScreenLastUser => "OnShareScreenLastUser";

        //Thông báo người nào đang share screen
        //SendAsync(OnUserIsSharingMsg, screenSharerUsername: string);
        public static string OnUserIsSharingMsg => "OnUserIsSharing";

        //Thông báo tình trạng muteCam của username. Chỉ dùng để thay đổi icon cam trên 
        //màn hình của người khác. Việc truyền cam là do peer trên FE quyết định
        //BE SendAsync(OnMuteCameraMsg, new { username: String, mute: bool })
        public static string OnMuteCameraMsg => "OnMuteCamera";

        //Thông báo tình trạng muteMic của username. Chỉ dùng để thay đổi icon mic trên 
        //màn hình của người khác. Việc truyền mic hay không là do peer trên FE quyết định
        //SendAsync(OnMuteMicroMsg, new { username: String, mute: bool })
        public static string OnMuteMicroMsg => "OnMuteMicro";

        //Thông báo có Chat Message mới
        //BE SendAsync("NewMessage", MessageSignalrGetDto)
        public static string NewMessageMsg => "NewMessage";

        //Thông báo có người yêu cầu dc vote
        // BE SendAsync(OnStartVoteMsg, ReviewSignalrDTO);
        public static string OnStartVoteMsg => "OnStartVote";

        //Thông báo có người yêu cầu dc vote
        // BE SendAsync("OnEndVote", username);
        public static string OnEndVoteMsg => "OnEndVote";

        //Thông báo Review có thay đổi
        //BE SendAsync(OnStartVoteMsg, ReviewSignalrDTO);
        public static string OnVoteChangeMsg => "OnVoteChange";

        //Thông báo để reload lại list vote của meeting
        //BE SendAsync(OnReloadVoteMsg, List<ReviewSignalrDTO>);
        public static string OnReloadVoteMsg => "OnReloadVote";

        public static string OnNewVoteResultMsg => "OnNewVoteResult";
        #endregion

        IMapper mapper;
        IHubContext<GroupHub> groupHub;
        PresenceTracker presenceTracker;
        IRepoWrapper repos;

        public MeetingHub(IRepoWrapper repos, PresenceTracker presenceTracker, IHubContext<GroupHub> presenceHubContext, IMapper mapper)
        {
            //Console.WriteLine("2.   " + new String('+', 50));
            //Console.WriteLine("2.   Hub/Chat: ctor(IUnitOfWork, UserShareScreenTracker, PresenceTracker, PresenceHub)");

            this.repos = repos;
            this.presenceTracker = presenceTracker;
            this.groupHub = presenceHubContext;
            this.mapper = mapper;
        }
        //sẽ dc gọi khi FE sẽ connect qua hàm này
        //sẽ dc gọi khi FE gọi:
        //this.chatHubConnection = new HubConnectionBuilder()
        //    .withUrl(this.hubUrl + 'chathub?roomId=' + roomId, {
        //        accessTokenFactory: () => user.token
        //    }).withAutomaticReconnect().build()
        //this.chatHubConnection.start().catch(err => console.log(err));
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("\n\n===========================\nOnConnectedAsync");
            //Step 1: Lấy meeting Id và username
            HttpContext httpContext = Context.GetHttpContext();
            string meetingIdString = httpContext.Request.Query["meetingId"].ToString();
            int meetingIdInt = int.Parse(meetingIdString);

            string isTempConnection = httpContext.Request.Query["tempConnection"].ToString();
            if (isTempConnection != null && isTempConnection.Length != 0 && isTempConnection == "ok")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, meetingIdString);
                //Clients.Caller.SendAsync("get-drawings", Drawings[meetingIdString]);
                base.OnConnectedAsync();
                return;
            }

            string username;
            int accountId;
            try
            {
                username = Context.User.GetUsername();
                accountId = Context.User.GetUserId();
            }
            catch
            {
                username = httpContext.Request.Query["username"].ToString();
                string acoountIdString = httpContext.Request.Query["accountId"].ToString();
                accountId = int.Parse(acoountIdString);
            }
            //Step 2: Add ContextConnection vào MeetingHub.Group(meetingId) và add (user, meeting) vào presenceTracker
            await presenceTracker.UserConnected(new UserConnectionSignalrDto(username, meetingIdInt), Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, meetingIdString);//khi user click vao room se join vao
                                                                                //await AddConnectionToGroup(meetingIdInt); // luu db DbSet<Connection> de khi disconnect biet

            //Step 3: Tạo Connect để lưu vào DB, ConnectionId
            #region lưu Db Connection
            Meeting meeting = await repos.Meetings.GetMeetingByIdSignalr(meetingIdInt);
            //Connection connection = new Connection(Context.ConnectionId, Context.User.GetUsername());
            Connection connection = new Connection
            {
                Id = Context.ConnectionId,
                //AccountId = Context.User.GetUserId(),
                AccountId = accountId,
                MeetingId = meetingIdInt,
                //UserName = Context.User.GetUsername(),
                UserName = username,
                Start = DateTime.Now
            };
            UserConnectionSignalrDto[] currentUsersInMeeting = await presenceTracker.GetOnlineUsersInMeet(meetingIdInt);
            if (meeting != null)
            {
                //meeting.Connections.Add(connection);
                repos.Connections.CreateConnectionSignalrAsync(connection);
                if (meeting.Start == null)
                {
                    meeting.Start = DateTime.Now;
                }
                meeting.CountMember = currentUsersInMeeting.Length;
                await repos.Meetings.UpdateAsync(meeting);
            }
            Console.WriteLine("++==++==++++++++++++++++");
            #endregion

            //var usersOnline = await _unitOfWork.UserRepository.GetUsersOnlineAsync(currentUsers);
            //Step 4: Thông báo với meetHub.Group(meetingId) là mày đã online  SendAsync(UserOnlineInGroupMsg, MemberSignalrDto)

            //MemberSignalrDto currentUserDto = await repos.Accounts.GetMemberSignalrAsync(username);
            //await Clients.Group(meetingIdString).SendAsync(UserOnlineInMeetingMsg, currentUserDto);

            var usersInMeeting = repos.Connections.GetList()
                .Where(e => e.MeetingId == meetingIdInt && e.End == null)
                .Select(e => e.UserName).ToHashSet();
            await Clients.Group(meetingIdString).SendAsync(UserOnlineInMeetingMsg, usersInMeeting);

            Console.WriteLine("2.1     " + new String('+', 50));
            Console.WriteLine("2.1     Hub/ChatSend: UserOnlineInGroupMsg, MemberSignalrDto");

            //Step 5: Update số người trong meeting lên db
            //UserConnectionSignalrDto[] currentUsersInMeeting = await presenceTracker.GetOnlineUsersInMeet(meetingIdInt);

            //await repos.Meetings.UpdateCountMemberSignalr(meetingIdInt, currentUsersInMeeting.Length);

            //Test
            await Clients.Group(meetingIdString).SendAsync(OnConnectMeetHubSuccessfullyMsg, $"Connect meethub dc r! {username} vô dc r ae ơi!!!");

            // Step 6: Thông báo với groupHub.Group(groupId) số người ở trong phòng  
            List<string> currentConnectionIds = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(username, meetingIdInt));
            Console.WriteLine("2.1     " + new String('+', 50));
            Console.WriteLine("2.1     Hub/PresenceSend: CountMemberInGroupMsg, { meetingId, countMember }");
            await groupHub.Clients.AllExcept(currentConnectionIds).SendAsync(GroupHub.CountMemberInGroupMsg,
                   new { meetingId = meetingIdInt, countMember = currentUsersInMeeting.Length });

            //Console.WriteLine("_+_+_+_+__+_+_+_+_+_+_+_+_+_+_+_++++++++++++++++++++++++\nConnect dc r !!!!!!!!!!!!!!!!!!!!!!!!!!");

            //Code xử lí db xóa duplicate connection
            IQueryable<Connection> dupCons = repos.Connections.GetList()
                .Where(c => c.Id != Context.ConnectionId && c.AccountId == Context.User.GetUserId() && c.End == null);
            foreach (var con in dupCons)
            {
                con.End = DateTime.Now;
                await repos.Connections.UpdateAsync(con);
            }
            await groupHub.Clients.Group(meeting.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //step 1: Lấy username 
            string username = Context.User.GetUsername();
            //step 2: Xóa connection trong db và lấy meeting
            Meeting meeting = await RemoveConnectionFromMeeting();

            //step 3: Xóa ContextConnectionId khỏi presenceTracker và check xem user còn connect nào khác với meeting ko
            bool isOffline = await presenceTracker.UserDisconnected(new UserConnectionSignalrDto(username, meeting.Id), Context.ConnectionId);

            //step 5: Remove ContextConnectionId khỏi meetingHub.Group(meetingId)   chắc move ra khỏi if
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, meeting.Id.ToString());
            if (isOffline)
            {
                //step 6: Nếu ko còn connect nào nữa thì Thông báo với meetingHub.Group(meetingId)
                MemberSignalrDto offLineUser = await repos.Accounts.GetMemberSignalrAsync(username);
                await Clients.Group(meeting.Id.ToString()).SendAsync(UserOfflineInMeetingMsg, offLineUser);

                //step 7: Update lại số người trong phòng
                UserConnectionSignalrDto[] currentUsersInMeeting = await presenceTracker.GetOnlineUsersInMeet(meeting.Id);
                await repos.Meetings.UpdateCountMemberSignalr(meeting.Id, currentUsersInMeeting.Length);

                //step 8: Thông báo với groupHub.Group(groupId) số người ở trong phòng
                await groupHub.Clients.All.SendAsync(GroupHub.CountMemberInGroupMsg,
                       new { meetingId = meeting.Id, countMember = currentUsersInMeeting.Length });
            }

            var usersInMeeting = repos.Connections.GetList()
               .Where(e => e.MeetingId == meeting.Id && e.End == null)
               .Select(e => e.UserName).ToHashSet();
            await Clients.Group(meeting.Id.ToString()).SendAsync(UserOnlineInMeetingMsg, usersInMeeting);

            if(usersInMeeting.Count == 0)
            {
                var usersJoined = repos.Connections.GetList()
                  .Where(e => e.MeetingId == meeting.Id)
                  .Select(e => e.UserName).ToHashSet();
                meeting.CountMember = usersJoined.Count;
                meeting.End = DateTime.Now;
                await repos.Meetings.UpdateAsync(meeting);
            }
            try
            {
                Connection connection = await repos.Connections.GetList().SingleOrDefaultAsync(e => e.Id == Context.ConnectionId);
                if (connection == null)
                {
                    Console.WriteLine("\n\n+++++++++++++\nEnd connection fail");
                }
                else
                {
                    connection.End = DateTime.Now;
                    await repos.Connections.UpdateAsync(connection);
                    //Hot fix duplicate connection
                    var dupConnections = repos.Connections.GetList()
                    .Where(e => e.AccountId == connection.AccountId && e.MeetingId == connection.MeetingId
                        && e.Start.Date == connection.Start.Date && e.Start.Hour == connection.Start.Hour
                        && e.Start.Minute == connection.Start.Minute);
                    foreach (var dupCon in dupConnections)
                    {
                        dupCon.End= DateTime.Now;
                        await repos.Connections.UpdateAsync(dupCon);
                    }
                }
            }
            catch
            {
                Console.WriteLine("\n\n+++++++++++++\nEnd connection fail");
            }

            //step 9: Disconnect khỏi meetHub
            await base.OnDisconnectedAsync(exception);
        }
        public async Task ShareScreenToUser(int meetingId, string receiverUsername, bool isShare)
        {
            var ReceiverConnectionIds = await presenceTracker.GetConnectionIdsForUser(new UserConnectionSignalrDto(receiverUsername, meetingId));
            if (ReceiverConnectionIds.Count > 0)
                await Clients.Clients(ReceiverConnectionIds).SendAsync(OnShareScreenMsg, isShare);
        }

        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('SendMessage', { content: string })
        public async Task SendMessageOld(MessageSignalrCreateDto createMessageDto)
        {
            string userName = Context.User.GetUsername();
            Account sender = await repos.Accounts.GetUserByUsernameSignalrAsync(userName);

            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);

            if (meeting != null)
            {
                MessageSignalrGetDto message = new MessageSignalrGetDto
                {
                    SenderUsername = userName,
                    SenderDisplayName = sender.Username,
                    Content = createMessageDto.Content,
                    MessageSent = DateTime.Now
                };
                //Luu message vao db
                Chat newChat = new Chat 
                {
                     Content=message.Content,
                     AccountId=Context.User.GetUserId(),
                     MeetingId=meeting.Id,
                     Time = message.MessageSent
                };
                await repos.Chats.CreateAsync(newChat);
                //code here
                //send meaasge to group
                await Clients.Group(meeting.Id.ToString()).SendAsync(NewMessageMsg, message);
            }
        }

        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('MuteMicro', mute)
        public async Task MuteMicro(bool muteMicro)
        {
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            if (meeting != null)
            {
                await Clients.Group(meeting.Id.ToString()).SendAsync(OnMuteMicroMsg, new { username = Context.User.GetUsername(), mute = muteMicro });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        //sẽ dc gọi khi người dùng muốn bật tắt cam
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('MuteCamera', mute: bool)
        //Thông báo cho cả meeting là có người bật tắt camera
        public async Task MuteCamera(bool muteCamera)
        {
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            if (meeting != null)
            {
                await Clients.Group(meeting.Id.ToString()).SendAsync(OnMuteCameraMsg, new { username = Context.User.GetUsername(), mute = muteCamera });
            }
            else
            {
                throw new HubException("group == null");
            }
        }
        //sẽ dc gọi khi có người xin dc vote (review)
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('StartVote', meetingId: int)
        public async Task StartVote(int meetingId)
        {
            string reviewee = Context.User.GetUsername();
            await Clients.Group(meetingId.ToString()).SendAsync(OnStartVoteMsg, reviewee);
        }

        //sẽ dc gọi khi có người xin dc vote (review)
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('StartVote', reviewee: username)
        public async Task EndVote(int meetingId)
        {
            int revieweeId = Context.User.GetUserId();
            Review newReview = new Review
            {
                MeetingId = meetingId,
                RevieweeId = revieweeId
            };
            await repos.Reviews.CreateAsync(newReview);
            ReviewSignalrDTO mapped = mapper.Map<ReviewSignalrDTO>(newReview);
            mapped.RevieweeUsername = Context.User.GetUsername();
            await Clients.Group(meetingId.ToString()).SendAsync(OnEndVoteMsg, mapped);

            var newMeetReviews = repos.Reviews.GetList()
                            .Where(e => e.MeetingId == meetingId)
                            .Include(e => e.Reviewee)
                            .Include(e => e.Details).ThenInclude(e => e.Reviewer);
            List<ReviewSignalrDTO> mappedNewReviews = newMeetReviews
                .ProjectTo<ReviewSignalrDTO>(mapper.ConfigurationProvider).ToList();
            await Clients.Group(meetingId.ToString()).SendAsync(OnReloadVoteMsg, mappedNewReviews);

        }

        //sẽ dc gọi khi có người xin dc vote (review)
        //sẽ dc gọi khi FE gọi chatHubConnection.invoke('VoteForReview', reviewDetail: ReviewDetailSignalrCreateDto)
        public async Task VoteForReview(int meetingId)
        {
            Console.WriteLine("2.   " + meetingId);
            await Clients.Group(meetingId.ToString()).SendAsync(OnVoteChangeMsg, "new");
        }
        private async Task<Meeting> RemoveConnectionFromMeeting()
        {
            Meeting meeting = await repos.Meetings.GetMeetingForConnectionSignalr(Context.ConnectionId);
            if(meeting == null)
            {
                int id = int.Parse(Context.GetHttpContext().Request.Query["meetingId"].ToString());
                meeting = repos.Meetings.GetList()
                    .Include(m=>m.Connections)
                    .SingleOrDefault(e => e.Id == id);
            }
            Connection? connection = repos.Connections.GetList()
                .SingleOrDefault(x => x.Id == Context.ConnectionId);
            if (connection != null) { 
                await repos.Meetings.EndConnectionSignalr(connection);
            }
            
            //hot fix duplicate connection
            var dupConnections = repos.Connections.GetList()
                .Where(e => e.AccountId == connection.AccountId && e.MeetingId == connection.MeetingId
                    && e.Start.Date == connection.Start.Date && e.Start.Hour == connection.Start.Hour
                    && e.Start.Minute == connection.Start.Minute && e.Id != connection.Id);
            foreach(var dupCon in dupConnections)
            {
                dupCon.End = DateTime.Now;
                await repos.Connections.UpdateAsync(dupCon);
            }

            IQueryable<Connection> activeConnections = repos.Meetings.GetActiveConnectionsForMeetingSignalr(meeting.Id);
            if (activeConnections.Count() == 0)
            {
                await repos.Meetings.EndMeetingSignalRAsync(meeting);
            }
            return meeting;
        }

        //////////////////////////////////////////
        /////TestOnly
        public async Task TestReceiveInvoke(string msg)
        {
            //int meetId = presenceTracker.
            Clients.Caller.SendAsync(OnTestReceiveInvokeMsg, "meehub invoke dc rồi ae ơi " + msg);
        }


        //public static readonly Dictionary<string, List<string>> Rooms = new Dictionary<string, List<string>>();
        public class IMessage
        {
            public string RoomId { get; set; }
            public string Content { get; set; }
            public string TimeStamp { get; set; }
            public string Username { get; set; }
            public string Author { get; set; }
        }
        public static readonly Dictionary<string, Dictionary<string, Peer>> Rooms = new Dictionary<string, Dictionary<string, Peer>>();
        public static readonly Dictionary<string, List<IMessage>> Chats = new Dictionary<string, List<IMessage>> ();
        public static readonly Dictionary<string, bool> IsSharing = new Dictionary<string, bool> ();
        public class CreateRoomInput
        {
            public string peerId { get; set; }
            public string username { get; set; }

            //public string roomId { get; set; } = "default";
            public string roomId { get; set; } = Guid.NewGuid().ToString();
        }

        public async Task CreateRoom()
        {
            HttpContext httpContext = Context.GetHttpContext();
            //string roomId = httpContext.Request.Query["meetingId"].ToString();
            string roomId  = Guid.NewGuid().ToString();

            Clients.All.SendAsync("room-created", roomId);
        }

        public class JoinRoomInput
        {
            public string roomId { get; set; }
            public string username { get; set; }
            public string peerId { get; set; }
        }
        public class Peer
        {
            public string peerId { get; set; }
            public string userName { get; set; }
        }
        public async Task JoinRoom(JoinRoomInput input)
        {

            //var input = JsonConvert.DeserializeObject<JoinRoomInput>(json);
            string roomId = input.roomId;
            string peerId = input.peerId;
            string username = input.username;

            Console.WriteLine($"\n\n==++==++===+++\n JoinRoom");
            Console.WriteLine(peerId);
            Console.WriteLine(roomId);
            Console.WriteLine(username);
            bool isRoomExisted = Rooms.ContainsKey(roomId);
            if (!isRoomExisted)
            {
                Rooms.Add(roomId, new Dictionary<string, Peer>());
            }
            bool isChatExisted = Chats.ContainsKey(roomId);
            if (!isChatExisted)
            {
                Chats.Add(roomId, new List<IMessage>());
            }
            bool isSharingExisted = IsSharing.ContainsKey(roomId);
            if (!isSharingExisted)
            {
                IsSharing.Add(roomId, false);
            }
            //bool isDrawExisted = Drawings.ContainsKey(roomId);
            //if (!isDrawExisted)
            //{
            //    Drawings.Add(roomId, new List<Drawing>());
            //}
            bool isUsernameExisted = Rooms[roomId].ContainsKey(username);
            Peer peer = new Peer
            {
                peerId = peerId,
                userName = username,
            };
            if (isUsernameExisted)
            {
                Rooms[roomId][username] = peer;
            }
            else
            {
                Rooms[roomId].Add(username, peer);
            }
            //await Groups.AddToGroupAsync(Context.ConnectionId, roomId);//khi user click vao room se join vao
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            //await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("user-joined", new{ roomId = roomId, peerId = peerId });
            //await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("user-joined", peer);
            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("user-joined", peer);
            await Clients.Group(roomId).SendAsync("get-users", new { roomId = roomId, participants = Rooms[roomId] });
            await Clients.Group(roomId).SendAsync("get-messages", Chats[roomId]);
        }

        public class LeaveRoomInput
        {
            public string roomId { get; set; }
            public string peerId { get; set; }
        }
        public async Task LeaveRoom(LeaveRoomInput input)
        {
            Console.WriteLine($"\n\n==++==++===+++\n LeaveRoom");
            Console.WriteLine(input.peerId);
            Console.WriteLine(input.roomId);
            string roomId = input.roomId;
            string peerId = input.peerId;
            string username = Context.User.GetUsername();
            Rooms[roomId].Remove(username);

            await Clients.GroupExcept(roomId, Context.ConnectionId).SendAsync("user-disconnected", peerId, username);

            //code mới xử lí db
            Meeting meeting = await RemoveConnectionFromMeeting();
            var usersInMeeting = repos.Connections.GetList()
               .Where(e => e.MeetingId == meeting.Id && e.End == null)
               .Select(e => e.UserName).ToHashSet();
            await Clients.Group(meeting.Id.ToString()).SendAsync(UserOnlineInMeetingMsg, usersInMeeting);
            if (usersInMeeting.Count == 0)
            {
                var usersJoined = repos.Connections.GetList()
                  .Where(e => e.MeetingId == meeting.Id)
                  .Select(e => e.UserName).ToHashSet();
                //meeting.CountMember = 5;
                meeting.CountMember = usersJoined.Count;
                meeting.End = DateTime.Now;
                await repos.Meetings.UpdateAsync(meeting);
                Rooms.Remove(roomId);
                Chats.Remove(roomId);
                //Drawings.Remove(roomId);
                DrawHub.Drawings.Remove(roomId);
            }
            else
            {
                meeting.CountMember = usersInMeeting.Count;
                await repos.Meetings.UpdateAsync(meeting);
            }

            Connection connection = await repos.Connections.GetList().SingleOrDefaultAsync(e => e.Id == Context.ConnectionId);
            if (connection == null)
            {
                Console.WriteLine("\n\n+++++++++++++\nEnd connection fail");
            }
            else
            {
                connection.End = DateTime.Now;
                await repos.Connections.UpdateAsync(connection);
            }
            await groupHub.Clients.Group(meeting.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);

        }

        public async Task SendMessage(IMessage message)
        {
            HttpContext httpContext = Context.GetHttpContext();
            int meetingId = int.Parse(httpContext.Request.Query["meetingId"].ToString());
            string roomId = message.RoomId;
            Chats[roomId].Add(message);
            Clients.Group(roomId).SendAsync("add-message", message);
            //Clients.Group(meetingId.ToString()).SendAsync("add-message", message);


            //xử lí db
            Chat newChat = new Chat {
                Content= message.Content,
                //MeetingId = int.Parse(roomId),
                MeetingId = meetingId,
                AccountId = Context.User.GetUserId(),
                Time = DateTime.Now,
            };
            await repos.Chats.CreateAsync(newChat);
        }
        public async Task StartSharing(JoinRoomInput input)
        {
            IsSharing[input.roomId] = true;
            Clients.Group(input.roomId).SendAsync("isSharing", IsSharing[input.roomId]);
            Clients.Group(input.roomId).SendAsync("user-started-sharing", IsSharing[input.roomId]);
        }

        public async Task StopSharing(string roomId)
        {
            IsSharing[roomId] = false;
            Clients.Group(roomId).SendAsync("isSharing", IsSharing[roomId]);
            Clients.Group(roomId).SendAsync("user-stopped-sharing", IsSharing[roomId]);
        }
        public async Task LeaderEndMeeting()
        {
            HttpContext httpContext = Context.GetHttpContext();
            int meetingId = int.Parse(httpContext.Request.Query["meetingId"].ToString());
            await Clients.Group(meetingId.ToString()).SendAsync("LeaderEndMeeting", "Nhóm trưởng đã kết thúc cuộc họp");
        }

        //public async Task Draw(int prevX, int prevY, int currentX, int currentY, string color, int size)
        //{
        //    HttpContext httpContext = Context.GetHttpContext();
        //    int meetingId = int.Parse(httpContext.Request.Query["meetingId"].ToString());
        //    Drawings[meetingId.ToString()]
        //        .Add(new Drawing {
        //            PrevX=prevX,
        //            PrevY=prevY, 
        //            CurrentX=currentX, 
        //            CurrentY=currentY, 
        //            Color=color,
        //            Size=size
        //        }
        //    );

        //    await Clients.GroupExcept(meetingId.ToString(), Context.ConnectionId).SendAsync("draw", prevX, prevY, currentX, currentY, color, size);
        //}

        //public static readonly Dictionary<string, List<Drawing>> Drawings = new Dictionary<string, List<Drawing>>();
        //public class Drawing
        //{
        //    public int PrevX { get; set; }
        //    public int PrevY { get; set; }
        //    public int CurrentX { get; set; }
        //    public int CurrentY { get; set; }
        //    public string Color { get; set; }
        //    public int Size { get; set; }
        //}
        public async Task TestLocaion(string msg)
        {
            Console.WriteLine($"\n\n\n\n==++==++===+++\n TestLocaion");
            Console.WriteLine(msg);

        }

    }
}
