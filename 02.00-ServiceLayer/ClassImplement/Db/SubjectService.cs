using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db
{
    internal class SubjectService : ISubjectService
    {
        private IRepoWrapper repos;

        public SubjectService(IRepoWrapper repos)
        {
            this.repos = repos;
        }

        public IQueryable<Subject> GetList()
        {
            return repos.Subjects.GetList();
        }
    }
}