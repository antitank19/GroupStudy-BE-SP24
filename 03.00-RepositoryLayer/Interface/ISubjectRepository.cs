using DataLayer.DBObject;

namespace RepositoryLayer.Interface
{
    public interface ISubjectRepository
    {
        IQueryable<Subject> GetList();
    }
}