using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class RequestReposity : BaseRepo<Request, int>, IRequestReposity
    {
        public RequestReposity(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Request entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Request> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Request> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Request entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}