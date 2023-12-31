using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class ReviewRepository : BaseRepo<Review, int>, IReviewRepository
    {
        public ReviewRepository(GroupStudyContext dbContext) : base(dbContext)
        {
        }

        public override Task CreateAsync(Review entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Review> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public override IQueryable<Review> GetList()
        {
            return base.GetList();
        }

        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Review entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}