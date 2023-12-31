using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class ScheduleRepository : BaseRepo<Schedule, int>, IScheduleRepository
    {
        public ScheduleRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Schedule entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Schedule> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Schedule> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Schedule entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}