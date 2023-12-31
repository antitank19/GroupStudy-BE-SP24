using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class ReviewDetailRepository : BaseRepo<ReviewDetail, int>, IReviewDetailRepository
    {
        public ReviewDetailRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(ReviewDetail entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<ReviewDetail> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<ReviewDetail> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(ReviewDetail entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}