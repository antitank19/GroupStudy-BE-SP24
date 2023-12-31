using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class GroupRepository : BaseRepo<Group, int>, IGroupRepository
    {
        public GroupRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Group entity)
        {
            return base.CreateAsync(entity);
        }

        public override async Task<Group> GetByIdAsync(int id)
        {
            return await dbContext.Groups
                .Include(e=>e.Class)
                .Include(e=>e.GroupSubjects).ThenInclude(e=>e.Subject)
                .Include(e=>e.Meetings).ThenInclude(m=>m.Chats).ThenInclude(c=>c.Account)
                .Include(e=>e.GroupMembers).ThenInclude(e=>e.Account)
                .Include(e=>e.JoinInvites).ThenInclude(e=>e.Account)
                .Include(e=>e.JoinRequests).ThenInclude(e=>e.Account)

                .Include(e => e.Meetings).ThenInclude(m=>m.Reviews).ThenInclude(c=>c.Reviewee)
                .Include(e => e.Meetings).ThenInclude(m=>m.Reviews).ThenInclude(c=>c.Details).ThenInclude(rd=>rd.Reviewer)
                .SingleOrDefaultAsync(e=>e.Id == id);
        }

        public override IQueryable<Group> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Group entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
