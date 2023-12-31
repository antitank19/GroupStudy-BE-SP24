using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBaseRepo<T, K>
    {
        public IQueryable<T> GetList();
        public Task<T> GetByIdAsync(K id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task RemoveAsync(K id);
        public Task<bool> IdExistAsync(K id);
    }
}
