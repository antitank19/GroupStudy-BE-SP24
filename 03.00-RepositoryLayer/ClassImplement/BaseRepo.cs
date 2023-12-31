using DataLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public abstract class BaseRepo<T, K> : IBaseRepo<T, K> where T: class
    {
        protected readonly GroupStudyContext dbContext;

        public BaseRepo(GroupStudyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IQueryable<T> GetList()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task<T> GetByIdAsync(K id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(K id)
        {
            T entity = await dbContext.Set<T>().FindAsync(id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IdExistAsync(K id)
        {
            return await dbContext.Set<T>().FindAsync(id) != null;
        }
    }
}
