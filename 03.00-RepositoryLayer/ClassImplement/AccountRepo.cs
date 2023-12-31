using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ShareResource.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class AccountRepo : BaseRepo<Account, int>, IAccountRepo
    {
        private readonly GroupStudyContext dbContext;
        private IMapper mapper;

        public AccountRepo(GroupStudyContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public override Task CreateAsync(Account entity)
        {
            return base.CreateAsync(entity);
        }

        public override Task<Account> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public async Task<Account> GetProfileByIdAsync(int id)
        {
            return await dbContext.Accounts
                 .Include(e => e.Role)
                 .Include(e => e.GroupMembers).ThenInclude(e => e.Group).ThenInclude(e=>e.GroupMembers)
                 .Include(e=>e.SupervisionsForStudent).ThenInclude(e=>e.Parent)
                 .Include(e=>e.SupervisionsForParent).ThenInclude(e=>e.Student)
                 .SingleOrDefaultAsync(e => e.Id == id);
        } 
        public async Task<Account> GetByUsernameAsync(string username)
        {
            return await dbContext.Accounts
                .Include(a => a.Role)
                .SingleOrDefaultAsync(a => a.Username == username);
        }

        public async Task<Account> GetByUsernameOrEmailAndPasswordAsync(string usernameOrEmail, string password)
        {
            return await dbContext.Accounts
                .Include(a=>a.Role)
                .SingleOrDefaultAsync(a => (a.Username == usernameOrEmail || a.Email == usernameOrEmail.ToLower()) && a.Password == password); 
        }

        public override IQueryable<Account> GetList()
        {
            return base.GetList();
        }


        public override Task RemoveAsync(int id)
        {
            return base.RemoveAsync(id);
        }

        public override Task UpdateAsync(Account entity)
        {
            return base.UpdateAsync(entity);
        }

        //SignalR
        //////////////////////////////////////////////////////////////////////
        public async Task<Account> GetUserByUsernameSignalrAsync(string username)
        {
            return await dbContext.Accounts.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<MemberSignalrDto> GetMemberSignalrAsync(string username)
        {

            return mapper.Map<MemberSignalrDto>(
                await dbContext.Accounts.SingleOrDefaultAsync(x => x.Username == username));
                //.ProjectTo<MemberSignalrDto>(mapper.ConfigurationProvider);
                //add CreateMap<AppUser, MemberDto>(); in AutoMapperProfiles
                //.SingleOrDefaultAsync();
        }
    }
}
