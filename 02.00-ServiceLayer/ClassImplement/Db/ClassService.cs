using DataLayer.DBObject;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.ClassImplement.Db
{
    public class ClassService : IClassService
    {
        private IRepoWrapper repos;

        public ClassService(IRepoWrapper repos)
        {
            this.repos = repos;
        }

        public IQueryable<Class> GetList()
        {
            return repos.Classes.GetList();
        }
    }
}