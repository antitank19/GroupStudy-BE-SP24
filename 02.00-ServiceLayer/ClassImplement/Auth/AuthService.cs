//using Google.Apis.Auth;
//using Google.Apis.Oauth2.v2;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using DataLayer.DBObject;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using ServiceLayer.Interface.Auth;
using ShareResource.APIModel;
using ShareResource.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.ClassImplement.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepoWrapper repos;
        private readonly IConfiguration configuration;
        private JwtSecurityTokenHandler jwtHandler;

        public AuthService(IRepoWrapper repos, IConfiguration configuration)
        {
            this.repos = repos;
            this.configuration = configuration;
            jwtHandler = new JwtSecurityTokenHandler();
        }

        public async Task<Account> LoginAsync(LoginModel loginModel)
        {
            return await repos.Accounts.GetByUsernameOrEmailAndPasswordAsync(loginModel.UsernameOrEmail, loginModel.Password);
        }

        public Task<Account> LoginWithGoogle(string googleIdToken)
        {
            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

            if (!jwtHandler.CanReadToken(googleIdToken))
            {
                throw new Exception("Invalidated googleIdToken");
            }


            var payload = GoogleJsonWebSignature.ValidateAsync(googleIdToken, new GoogleJsonWebSignature.ValidationSettings()).Result;
            return repos.Accounts.GetByUsernameAsync(payload.Email);
        }

        public async Task<string> GenerateJwtAsync(Account logined, bool rememberMe)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, logined.Id.ToString()),
                new Claim(ClaimTypes.Name, logined.Username),
                new Claim(ClaimTypes.Email, logined.Email),
                new Claim(ClaimTypes.Role, logined.Role.Name),
                //new Claim("Email", logined.Email),
                //new Claim("Role", logined.Role.Name)
            };
            var issuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(configuration["Authentication:JwtToken:TokenKey"]));
            var credential = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                configuration["Authentication:JwtToken:Issuer"],
                configuration["Authentication:JwtToken:Audience"],
                claims,
                expires: rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(1),
                signingCredentials: credential
            );
            return jwtHandler.WriteToken(jwtSecurityToken);
        }

        public async Task Register(Account register, RoleNameEnum role)
        {
            register.RoleId = (int)role;
            await repos.Accounts.CreateAsync(register);
        }
    }
}
