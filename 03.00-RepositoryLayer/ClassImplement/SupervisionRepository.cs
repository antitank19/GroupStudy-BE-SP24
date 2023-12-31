using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class SupervisionRepository : BaseRepo<Supervision, int>, ISupervisionRepository
    {
        public SupervisionRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Supervision entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Supervision> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Supervision> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Supervision entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}