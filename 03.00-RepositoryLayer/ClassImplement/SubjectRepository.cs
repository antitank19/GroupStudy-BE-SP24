using DataLayer.DBContext;
using DataLayer.DBObject;
using RepositoryLayer.Interface;

namespace RepositoryLayer.ClassImplement
{
    internal class SubjectRepository : ISubjectRepository
    {
        private GroupStudyContext dbContext;

        public SubjectRepository(GroupStudyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Subject> GetList()
        {
           return dbContext.Subjects.AsQueryable();
        }
    }
}