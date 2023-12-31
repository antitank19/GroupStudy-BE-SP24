using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface IClassReposity
    {
        public IQueryable<Class> GetList();
    }
}