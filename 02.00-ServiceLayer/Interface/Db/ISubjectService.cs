using DataLayer.DBObject;

namespace ServiceLayer.Interface.Db
{
    public interface ISubjectService
    {
        IQueryable<Subject> GetList();
    }
}