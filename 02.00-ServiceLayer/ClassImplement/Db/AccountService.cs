using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Db;
using ShareResource.Enums;

namespace ServiceLayer.ClassImplement.Db
{
    public class AccountService : IAccountService
    {
        private IRepoWrapper repos;

        public AccountService(IRepoWrapper repos)
        {
            this.repos = repos;
        }
        public IQueryable<Account> GetList()
        {
            return repos.Accounts.GetList();
        }
        public IQueryable<Account> SearchStudents(string search, int? groupId, int? parentId)
        {
            search = search.ToLower().Trim();
            if(parentId.HasValue)
            {
                return repos.Accounts.GetList()
               .Include(e => e.GroupMembers).ThenInclude(e => e.Group)
               .Where(e =>
                    e.RoleId == (int)RoleNameEnum.Student
                    && !e.SupervisionsForStudent.Any(e => e.ParentId == parentId)
                    && (EF.Functions.Like(e.Id.ToString(), search + "%")
                    || e.Email.ToLower().Contains(search)
                    || e.Username.ToLower().Contains(search)
                    || e.FullName.ToLower().Contains(search))
               );
            }
            if (groupId.HasValue)
            {
                //return repos.GroupMembers.GetList().Where(e=>e.GroupId!=groupId).Include(e => e.Account).Select(e=>e.Account)
                //.Where(e =>
                //    EF.Functions.Like(e.Id.ToString(), search + "%")
                //    //e.Id.ToString().Contains(search)
                //    //SqlFunctions.StringConvert((double)e.Id) 
                //    || e.Email.ToLower().Contains(search)
                //    || e.Username.ToLower().Contains(search)
                //    || e.FullName.ToLower().Contains(search)
                //);
                return repos.Accounts.GetList()
                .Include(e=>e.GroupMembers).ThenInclude(e=>e.Group)
                .Where(e =>
                    e.RoleId == (int)RoleNameEnum.Student
                    && !e.GroupMembers.Any(e=>e.GroupId==groupId)
                    && (EF.Functions.Like(e.Id.ToString(), search + "%")
                    || e.Email.ToLower().Contains(search)
                    || e.Username.ToLower().Contains(search)
                    || e.FullName.ToLower().Contains(search))
                );
            }
            return repos.Accounts.GetList()
                .Include(e=>e.GroupMembers).ThenInclude(e=>e.Group)
                .Where(e =>
                    e.RoleId == (int)RoleNameEnum.Student
                    && (EF.Functions.Like(e.Id.ToString(), search+"%")
                    //e.Id.ToString().Contains(search)
                    //SqlFunctions.StringConvert((double)e.Id) 
                    || e.Email.ToLower().Contains(search)
                    || e.Username.ToLower().Contains(search)
                    || e.FullName.ToLower().Contains(search))
                );
        }


        public async Task<Account> GetByIdAsync(int id)
        {
            return await repos.Accounts.GetByIdAsync(id);
        }

        public async Task<Account> GetProfileByIdAsync(int id)
        {
            return await repos.Accounts.GetProfileByIdAsync(id);
        }

        public async Task<Account> GetAccountByUserNameAsync(string userName)
        {
            Account account = await repos.Accounts.GetByUsernameAsync(userName);
            return account;
        }

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            Account account = await repos.Accounts.GetList()
                .SingleOrDefaultAsync(e=>e.Email==email);
            return account;
        }

        public async Task CreateAsync(Account entity)
        {
            await repos.Accounts.CreateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await repos.Accounts.RemoveAsync(id);
        }

        public async Task UpdateAsync(Account entity)
        {
            await repos.Accounts.UpdateAsync(entity);
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await repos.Accounts.IdExistAsync(id);
        }

        public async Task<bool> ExistUsernameAsync(string username)
        {
            return await repos.Accounts.GetList().AnyAsync(x => x.Username == username);
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            return await repos.Accounts.GetList().AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsParentStudentRelated(int parentId, int studentId)
        {
            return await repos.Supervisions.GetList()
                .AnyAsync(e => e.ParentId == parentId && e.StudentId == studentId);
        }

        public async Task<Supervision> GetParentStudentRelationAsync(int parentId, int studentId)
        {
            return await repos.Supervisions.GetList()
                .SingleOrDefaultAsync(e => e.ParentId == parentId && e.StudentId == studentId);
        }

        public async Task<Supervision> CreateSuperviseRequestAsync(int parentId, int studentId)
        {
            Supervision created = new Supervision()
            {
                ParentId = parentId,
                StudentId = studentId,
                State=RequestStateEnum.Waiting
            };
            await repos.Supervisions.CreateAsync(created);
            return created;
        }

        public IQueryable<Supervision> GetWaitingSupervisionForStudent(int studentId)
        {
            return repos.Supervisions.GetList()
                .Where(e => e.StudentId == studentId&&e.State==RequestStateEnum.Waiting);
        }

        public async Task<Supervision> GetWaitingSupervisionByIdAsync(int supervisionId)
        {
            return await repos.Supervisions.GetByIdAsync(supervisionId);
        }

        public async Task<Supervision> GetSupervisionByIdAsync(int supervisionId)
        {
            return await repos.Supervisions.GetByIdAsync(supervisionId);
        }

        public async Task UpdateSupervisionAsync(Supervision updated)
        {
            await repos.Supervisions.UpdateAsync(updated);
        }

        public async Task<bool> DeleteSupervisionAsync(Supervision delete)
        {
            try
            {
                await repos.Supervisions.RemoveAsync(delete.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Account> GetParentsOfStudent(int studentId)
        {
            return repos.Accounts.GetList()
                .Where(a => a.SupervisionsForParent.Any(s => s.StudentId == studentId));
        }

        public IQueryable<Account> GetStudentsOfParent(int parentId)
        {
            return repos.Accounts.GetList()
                .Where(a => a.SupervisionsForStudent.Any(s => s.ParentId == parentId));
        }

        public IQueryable<Supervision> GetAcceptedSupervisionForStudent(int studentId)
        {
            return repos.Supervisions.GetList()
                 .Where(e => e.StudentId == studentId && e.State == RequestStateEnum.Approved);
        }

        public IQueryable<Supervision> GetAcceptedSupervisionForParent(int parentId)
        {
            return repos.Supervisions.GetList()
                  .Where(e => e.ParentId == parentId && e.State == RequestStateEnum.Approved);
        }
    }
}
