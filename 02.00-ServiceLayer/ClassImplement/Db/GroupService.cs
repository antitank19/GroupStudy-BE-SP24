using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.DTO;
using ShareResource.Enums;
using ShareResource.Utils;
using ShareResource.UpdateApiExtension;

namespace ServiceLayer.ClassImplement.Db
{
    public class GroupService  : IGroupService
    {
        private IRepoWrapper repos;

        public GroupService(IRepoWrapper repos)
        {
            this.repos = repos;
        }
        public IQueryable<Group> GetList()
        {
            return repos.Groups.GetList();
        }

        public async Task<IQueryable<Group>> SearchGroups(string search, int studentId, bool newGroup)
        {
            search=search.ConvertToUnsign().ToLower();
            if (newGroup)
            {
                return repos.Groups.GetList()
                    .Include(e => e.GroupMembers)
                    .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                    .Where(e =>
                        !e.GroupMembers.Any(e => e.AccountId == studentId)
                        && ((EF.Functions.Like(e.Id.ToString(), search + "%"))
                        || e.Name.ConvertToUnsign().ToLower().Contains(search)
                        || search.Contains(e.ClassId.ToString())
                        || e.GroupSubjects.Any(gs => gs.Subject.Name.ConvertToUnsign().ToLower().Contains(search))));
            }
            return repos.Groups.GetList()
                .Include(e => e.GroupMembers)
                .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                .Where(e =>
                    (EF.Functions.Like(e.Id.ToString(), search + "%"))
                    || e.Name.ConvertToUnsign().ToLower().Contains(search)
                    || search.Contains(e.ClassId.ToString())
                    || e.GroupSubjects.Any(gs=>gs.Subject.Name.ConvertToUnsign().ToLower().Contains(search)));
            //return repos.Accounts.GetList().Include(e=>e.GroupMembers)
            //    .Where(e =>
            //        !e.GroupMembers.Any(e=>e.GroupId==groupId)
            //        &&(EF.Functions.Like(e.Id.ToString(), search + "%")
            //        || e.Email.ToLower().Contains(search)
            //        || e.Username.ToLower().Contains(search)
            //        || e.FullName.ToLower().Contains(search))
            //    );
        }

        public async Task<IQueryable<Group>> SearchGroupsBySubject(string search, int studentId, bool newGroup)
        {
            search = search.ConvertToUnsign().ToLower();
            if (newGroup)
            {
                return repos.Groups.GetList()
                    .Include(e => e.GroupMembers)
                    .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                    .Where(e =>!e.GroupMembers.Any(e => e.AccountId == studentId)
                        && e.GroupSubjects.Any(gs => gs.Subject.Name.ConvertToUnsign().ToLower().Contains(search)));
            }
            return repos.Groups.GetList()
                .Include(e => e.GroupMembers)
                .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                .Where(e => e.GroupSubjects.Any(gs => gs.Subject.Name.ConvertToUnsign().ToLower().Contains(search)));
        }

        public async Task<IQueryable<Group>> SearchGroupsByClass(string search, int studentId, bool newGroup)
        {
            search = search.ConvertToUnsign().ToLower();
            if (newGroup)
            {
                return repos.Groups.GetList()
                    .Include(e => e.GroupMembers)
                    .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                    .Where(e => !e.GroupMembers.Any(e => e.AccountId == studentId)
                        && search.Contains(e.ClassId.ToString()));
            }
            return repos.Groups.GetList()
                .Include(e => e.GroupMembers)
                .Include(e => e.GroupSubjects).ThenInclude(e => e.Subject)
                .Where(e => search.Contains(e.ClassId.ToString()));
        }

        public async Task<IQueryable<Group>> GetJoinGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.IsActive == true)
                .Select(e => e.Group);
        }

        public async Task<IQueryable<Group>> GetMemberGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.MemberRole == GroupMemberRole.Member && e.IsActive == true)
                .Select(e => e.Group);
        }

        public async Task<IQueryable<Group>> GetLeaderGroupsOfStudentAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e=>e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.MemberRole == GroupMemberRole.Leader && e.IsActive == true)
                .Select(e => e.Group);
        }

        public async Task<Group> GetFullByIdAsync(int id)
        {
            return await repos.Groups.GetByIdAsync(id);
        }


        public async Task CreateAsync(Group entity, int creatorId)
        {
            entity.GroupMembers.Add(new GroupMember { 
                AccountId = creatorId,
                MemberRole=GroupMemberRole.Leader
            });
            await repos.Groups.CreateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await repos.Groups.RemoveAsync(id);
        }

        //public async Task UpdateAsync(Group entity)
        //{
        //    await repos.Groups.UpdateAsync(entity);
        //}

        public async Task UpdateAsync(GroupUpdateDto dto)
        {
            var group = await repos.Groups.GetByIdAsync(dto.Id);
            //Remove subject, nếu dto ko có thì sẽ loại
            group.PatchUpdate<Group, GroupUpdateDto>(dto);
            List<GroupSubject> groupSubjects = group.GroupSubjects.ToList();
            foreach (GroupSubject groupSubject in groupSubjects)
            {
                if (!dto.SubjectIds.Cast<int>().Contains(groupSubject.SubjectId))
                {
                    group.GroupSubjects.Remove(groupSubject);
                }
            }
            //Add new subject, nếu group ko có thì add
            foreach (int subjectId in dto.SubjectIds)
            {
                 if(!group.GroupSubjects.Any(e=>e.SubjectId == subjectId))
                {
                    group.GroupSubjects.Add(new GroupSubject { GroupId = group.Id, SubjectId = subjectId });
                }
            }
            //if (dto.())
            //{
            //    repos.G
            //}
                //group.GroupSubjects = dto.SubjectIds.Select(subId => new GroupSubject { GroupId = dto.Id, SubjectId = (int)subId }).ToList();
            await repos.Groups.UpdateAsync(group);
        }

        public async Task<List<int>> GetLeaderGroupsIdAsync(int studentId)
        {
            return repos.GroupMembers.GetList()
                .Include(e => e.Group).ThenInclude(e => e.GroupMembers)
                .Where(e => e.AccountId == studentId && e.MemberRole == GroupMemberRole.Leader && e.IsActive == true)
                .Select(e => e.GroupId).ToList();
        }

        public async Task<bool> IsStudentLeadingGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
                .AnyAsync(e=>e.AccountId==studentId && e.GroupId==groupId && e.MemberRole == GroupMemberRole.Leader
                     && e.IsActive == true);
        }

        public async Task<bool> IsStudentMemberGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
                .AnyAsync(e => e.AccountId == studentId && e.GroupId == groupId && e.MemberRole == GroupMemberRole.Member
                     && e.IsActive == true);
        }

        public async Task<bool> IsStudentJoiningGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
               .AnyAsync(e => e.AccountId == studentId && e.GroupId == groupId
                    && e.IsActive == true);
        }
           //Fix later
        public async Task<bool> IsStudentRequestingToGroupAsync(int studentId, int groupId)
        {
            return await repos.Requests.GetList()
              .AnyAsync(e => e.AccountId == studentId && e.GroupId == groupId 
                && (e.State == RequestStateEnum.Waiting)
              );
        }
         //Fix later
        public async Task<bool> IsStudentInvitedToGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
              .AnyAsync(e => e.AccountId == studentId && e.GroupId == groupId && e.IsActive == true
              //&& (e.State == GroupMemberState.Inviting)
              );
        }

        public async Task<bool> IsStudentBannedToGroupAsync(int studentId, int groupId)
        {
            return await repos.GroupMembers.GetList()
                .AnyAsync(e => e.AccountId == studentId && e.GroupId == groupId && e.IsActive == false);
        }

        public async Task<bool> ExistsAsync(int groupId)
        {
            return await repos.Groups.IdExistAsync(groupId);
        }
    }
}
