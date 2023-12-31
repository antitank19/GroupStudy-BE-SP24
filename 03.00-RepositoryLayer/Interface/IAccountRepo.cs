using DataLayer.DBObject;
using ShareResource.DTO;

namespace RepositoryLayer.Interface
{
    public interface IAccountRepo : IBaseRepo<Account, int>
    {
        Task<Account> GetByUsernameAsync(string email);
        Task<Account> GetByUsernameOrEmailAndPasswordAsync(string usernameOrEmail, string password);
        Task<Account> GetProfileByIdAsync(int id);
        Task<MemberSignalrDto> GetMemberSignalrAsync(string username);
        Task<Account> GetUserByUsernameSignalrAsync(string username);
    }
}