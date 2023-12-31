using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.DTO;
using ShareResource.Enums;
using System.Text.RegularExpressions;

namespace ServiceLayer.ClassImplement.Db
{
    internal class GroupMemberSerivce : IGroupMemberSerivce
    {
        private IRepoWrapper repos;
        private IMapper mapper;

        public GroupMemberSerivce(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await repos.GroupMembers.GetList().AnyAsync(x => x.Id == id);
        }

        public async Task<GroupMember> GetByIdAsync(int inviteId)
        {
            return await repos.GroupMembers.GetList().FirstOrDefaultAsync(x => x.Id == inviteId);
        }

        public IQueryable<AccountProfileDto> GetMembersJoinForGroup(int groupId)
        {
            IQueryable<Account> list = repos.GroupMembers.GetList()
                .Where(e => e.GroupId == groupId && e.IsActive == true)
                .Include(e => e.Account)
                .Select(e => e.Account);
            return list.ProjectTo<AccountProfileDto>(mapper.ConfigurationProvider);
        }
        //Fix later
        public IQueryable<JoinRequestForGroupGetDto> GetJoinRequestForGroup(int groupId)
        {
            IQueryable<Request> list = repos.Requests.GetList()
                .Where(e => e.GroupId == groupId && e.State == RequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            //Console.WriteLine("+_+_+_+_+_+_ " + list.Count());
            return list.ProjectTo<JoinRequestForGroupGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<JoinInviteForGroupGetDto> GetJoinInviteForGroup(int groupId)
        {
            IQueryable<Invite> list = repos.Invites.GetList()
                .Where(e => e.GroupId == groupId && e.State == RequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group);
            return list.ProjectTo<JoinInviteForGroupGetDto>(mapper.ConfigurationProvider);
        }


        public IQueryable<JoinRequestForStudentGetDto> GetJoinRequestForStudent(int studentId)
        {
            IQueryable<Request> list = repos.Requests.GetList()
                .Where(e => e.AccountId == studentId && e.State == RequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group).ThenInclude(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers);
            IQueryable<JoinRequestForStudentGetDto> mapped = list.ProjectTo<JoinRequestForStudentGetDto>(mapper.ConfigurationProvider);
            return mapped;
        }

        public async Task<Invite> GetInviteOfStudentAndGroupAsync(int accountId, int groupId)
        {
            Invite invite = await repos.Invites.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.AccountId == accountId
                    && e.GroupId == groupId && e.State == RequestStateEnum.Waiting);
            return invite;

        }

        public async Task<Request> GetRequestOfStudentAndGroupAsync(int accountId, int groupId)
        {
            Request request = await repos.Requests.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.AccountId == accountId
                    && e.GroupId == groupId && e.State == RequestStateEnum.Waiting);
            return request;
        }

        public async Task<Invite> GetInviteByIdAsync(int inviteId)
        {
            Invite invite = await repos.Invites.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.Id == inviteId);
            return invite;
        }

        public async Task<Request> GetRequestByIdAsync(int requestId)
        {
            Request request = await repos.Requests.GetList()
                .Include(e => e.Account)
                .Include(e => e.Group)
                .SingleOrDefaultAsync(e => e.Id == requestId);
            return request;
        }


        public IQueryable<JoinInviteForStudentGetDto> GetJoinInviteForStudent(int studentId)
        {
            IQueryable<Invite> list = repos.Invites.GetList()
                .Where(e => e.AccountId == studentId && e.State == RequestStateEnum.Waiting)
                .Include(e => e.Account)
                .Include(e => e.Group).ThenInclude(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers);
            return list.ProjectTo<JoinInviteForStudentGetDto>(mapper.ConfigurationProvider);
        }

        public async Task CreateJoinInvite(GroupMemberInviteCreateDto dto)
        {
            //GroupMember invite = mapper.Map<GroupMember>(dto);
            //await repos.GroupMembers.CreateAsync(invite);
            Invite invite = mapper.Map<Invite>(dto);
            await repos.Invites.CreateAsync(invite);
        }

        public async Task CreateJoinRequest(GroupMemberRequestCreateDto dto)
        {
            //GroupMember request = mapper.Map<GroupMember>(dto);
            //await repos.GroupMembers.CreateAsync(request);
            Request request = mapper.Map<Request>(dto);
            await repos.Requests.CreateAsync(request);
        }

        public async Task<GroupMember> GetGroupMemberOfStudentAndGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
               .SingleOrDefaultAsync(e => e.AccountId == studentId && e.GroupId == groupId);
        }

        public async Task AcceptOrDeclineInviteAsync(Invite existedInvite, bool isAccepted)
        {
            //if (existed.State != GroupMemberState.Inviting)
            //{
            //    throw new Exception("Đây không phải là thư mời");
            //}
            //existed.State = isAccepted ? GroupMemberState.Member : GroupMemberState.Banned;
            //await repos.GroupMembers.UpdateAsync(existed);
            existedInvite.State = isAccepted ? RequestStateEnum.Approved : RequestStateEnum.Decline;
            await repos.Invites.UpdateAsync(existedInvite);
            if (isAccepted)
            {
                GroupMember newMember = new GroupMember
                {
                    AccountId = existedInvite.AccountId,
                    GroupId = existedInvite.GroupId,
                    MemberRole = GroupMemberRole.Member,
                    IsActive = true,
                };
                await repos.GroupMembers.CreateAsync(newMember);
            }
        }

        public async Task AcceptOrDeclineRequestAsync(Request existedRequest, bool isAccepted)
        {
            //if (existed.State != InviteRequestStateEnum.Waiting)
            //{
            //    throw new Exception("Yêu cầu đã được xử lí");
            //}
            existedRequest.State = isAccepted ? RequestStateEnum.Approved : RequestStateEnum.Decline;
            await repos.Requests.UpdateAsync(existedRequest);
            if (isAccepted)
            {
                GroupMember newMember = new GroupMember
                {
                    AccountId = existedRequest.AccountId,
                    GroupId = existedRequest.GroupId,
                    MemberRole = GroupMemberRole.Member,
                    IsActive = true,
                };
                await repos.GroupMembers.CreateAsync(newMember);
            }
        }

        public async Task BanUserFromGroupAsync(GroupMember banned)
        {
            //banned.IsActive = false;
            //await repos.GroupMembers.UpdateAsync(banned);
            await repos.GroupMembers.RemoveAsync(banned.Id);
        }
    }
}