using DataLayer.DBObject;
using ShareResource.APIModel;
using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface.Auth
{
    public interface IAuthService
    {
        /// <summary>
        ///    LoginAsync by username or mail and password
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>jwtToken: String</returns>
        public Task<Account> LoginAsync(LoginModel loginModel);
        /// <summary>
        /// LoginAsync by google
        /// </summary>
        /// <param name="googleIdToken"></param>
        /// <returns>jwtToken: String</returns>
        public Task<Account> LoginWithGoogle(string googleIdToken);
        public Task<string> GenerateJwtAsync(Account logined, bool rememberMe=true);
        public Task Register(Account register, RoleNameEnum role);
    }
}
