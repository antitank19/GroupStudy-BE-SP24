using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class ClassRepository : IClassReposity
    {
        private GroupStudyContext dbContext;

        public ClassRepository(GroupStudyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Class> GetList()
        {
            return dbContext.Classes.AsQueryable();
        }
    }
}