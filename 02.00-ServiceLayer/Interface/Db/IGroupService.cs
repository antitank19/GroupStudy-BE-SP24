using DataLayer.DBObject;
using ShareResource.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Db
{
    public interface IGroupService
    {
        public IQueryable<Group> GetList();
        public Task<Group> GetFullByIdAsync(int id);
        /// <summary>
        /// Create a group and add group leader
        /// </summary>
        /// <param name="group"></param>
        /// <param name="creatorId">id of creator account id</param>
        /// <returns></returns>
        public Task CreateAsync(Group group, int creatorId);
        //public Task UpdateAsync(Group group);
        public Task UpdateAsync(GroupUpdateDto dto);
        /// <summary>
        /// DO NOT USE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task RemoveAsync(int id);
        /// <summary>
        /// Get all groups student (member) has joined
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<IQueryable<Group>> GetMemberGroupsOfStudentAsync(int studentId);
        /// <summary>
        /// Get all groups leader has created
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<IQueryable<Group>> GetLeaderGroupsOfStudentAsync(int leaderId);
        /// <summary>
        /// Get all groups student (leader, member) has joined
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Task<IQueryable<Group>> GetJoinGroupsOfStudentAsync(int studentId);
        public Task<List<int>> GetLeaderGroupsIdAsync(int leaderId);
        /// <summary>
        /// Check if a student is a leader of a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentLeadingGroupAsync(int studentId, int groupId);
        /// <summary>
        /// Check if a student is a member (member only) of a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentMemberGroupAsync(int studentId, int groupId);
        /// <summary>
        /// Check if a student (member, leader) join a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentJoiningGroupAsync(int studentId, int groupId);
        /// <summary>
        /// Check if a student is requesting to join a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentRequestingToGroupAsync(int studentId, int groupId);
        /// <summary>
        /// Check if a student is invited to join a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentInvitedToGroupAsync(int studentId, int groupId);
        /// <summary>
        /// Check if a student is invited to join a group
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Task<bool> IsStudentBannedToGroupAsync(int studentId, int groupId);
        /// <summary>
        /// search by name, class, subject
        /// </summary>
        /// <param name="search"></param>
        /// <param name="studentId"></param>
        /// <param name="newGroup"></param>
        /// <returns></returns>
        public Task<IQueryable<Group>> SearchGroups(string search, int studentId, bool newGroup);
        public Task<IQueryable<Group>> SearchGroupsByClass(string search, int studentId, bool newGroup);
        public Task<IQueryable<Group>> SearchGroupsBySubject(string search, int studentId, bool newGroup);
        public Task<bool> ExistsAsync(int groupId);
    }
}
