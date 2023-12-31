using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class GroupMemberReposity : BaseRepo<GroupMember, int>, IGroupMemberReposity
    {
        public GroupMemberReposity(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(GroupMember entity)
        {
            return base.CreateAsync(entity);
        }

        public override async Task<GroupMember> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public override IQueryable<GroupMember> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(GroupMember entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}