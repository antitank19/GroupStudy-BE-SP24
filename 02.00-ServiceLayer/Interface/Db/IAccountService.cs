using DataLayer.DBObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Db
{
    public interface IAccountService 
    {
        public IQueryable<Account> GetList();
        public Task<Account> GetByIdAsync(int id);
        /// <summary>
        /// Create a group and add group leader
        /// </summary>
        /// <param name="account"></param>
        /// <param name="creatorId">id of creator account id</param>
        /// <returns></returns>
        public Task CreateAsync(Account account);
        public Task UpdateAsync(Account account);
        public Task RemoveAsync(int id);
        public Task<Account> GetAccountByUserNameAsync(string userName);
        public Task<Account> GetAccountByEmailAsync(string email);
        public Task<Account> GetProfileByIdAsync(int id);
        public IQueryable<Account> SearchStudents(string search, int? groupId, int? parentId);
        public Task<bool> ExistAsync(int id);
        public Task<bool> ExistUsernameAsync(string username);
        public Task<bool> ExistEmailAsync(string email);
        public Task<bool> IsParentStudentRelated(int parentId, int studentId);
        public Task<Supervision> GetParentStudentRelationAsync(int parentId, int studentIsd);
        public Task<Supervision> CreateSuperviseRequestAsync(int parentId, int studentId);
        public IQueryable<Supervision> GetWaitingSupervisionForStudent(int parentId);
        public Task<Supervision> GetWaitingSupervisionByIdAsync(int supervisionId);
        public Task<Supervision> GetSupervisionByIdAsync(int supervisionId);
        public Task UpdateSupervisionAsync(Supervision updated);
        public IQueryable<Account> GetParentsOfStudent(int studentId);
        public IQueryable<Account> GetStudentsOfParent(int parentId);
        public IQueryable<Supervision> GetAcceptedSupervisionForStudent(int studentId);
        public IQueryable<Supervision> GetAcceptedSupervisionForParent(int parentId);
        public Task<bool> DeleteSupervisionAsync(Supervision delete);
    }
}
