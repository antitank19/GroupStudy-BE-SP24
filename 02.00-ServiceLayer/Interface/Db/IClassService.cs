using DataLayer.DBObject;
using Microsoft.AspNetCore.Mvc;

namespace ServiceLayer.Interface.Db
{
    public interface IClassService
    {
        public IQueryable<Class> GetList();
    }
}