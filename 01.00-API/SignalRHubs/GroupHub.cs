using API.SignalRHub.Tracker;
using APIExtension.ClaimsPrinciple;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ShareResource.DTO.Connection;

namespace API.SignalRHub
{
    /// <summary>
    /// Use to count number of ppl in rooms
    /// </summary>
    [Authorize]
    public class GroupHub : Hub
    {
        //BE: SendAsync(GroupHub.CountMemberInGroupMsg, new { meetingId: int, countMember: int })
        public static string CountMemberInGroupMsg => "CountMemberInGroup";
        public static string OnLockedUserMsg => "OnLockedUser";
        public static string OnReloadMeetingMsg => "OnReloadMeeting";

        private readonly PresenceTracker presenceTracker;
        public GroupHub(PresenceTracker tracker)
        {
            presenceTracker = tracker;
        }
        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            string groupIdString = httpContext.Request.Query["groupId"].ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupIdString);

            var isOnline = await presenceTracker.UserConnected(new UserConnectionSignalrDto(Context.User.GetUsername(), 0), Context.ConnectionId);
            base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            HttpContext httpContext = Context.GetHttpContext();
            string groupIdString = httpContext.Request.Query["groupId"].ToString();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupIdString);

            var isOffline = await presenceTracker.UserDisconnected(new UserConnectionSignalrDto(Context.User.GetUsername(), 0), Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        //TestOnly
        public async Task TestReceiveInvoke(string msg)
        {
            Console.WriteLine("+++++++++++==================== " + msg + " group ReceiveInvoke successfull");
            //int meetId = presenceTracker.
            Clients.Caller.SendAsync("OnTestReceiveInvoke", "group invoke dc rồi ae ơi " + msg);
        }
    }
}
