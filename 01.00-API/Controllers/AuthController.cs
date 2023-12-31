using APIExtension.Auth;
using APIExtension.Const;
using APIExtension.HttpContext;
using APIExtension.Validator;
using AutoMapper;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ShareResource.APIModel;
using ShareResource.DTO;
using ShareResource.Enums;
using ShareResource.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;
        private readonly IValidatorWrapper validators;
        public AuthController(IServiceWrapper services, IMapper mapper, IValidatorWrapper validators)
        {
            this.services = services;
            this.mapper = mapper;
            this.validators = validators;
        }



        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.True}] Login for student with username or email. Return JWT Token",
            Description = "Login for student with username or email. Return JWT Token if successfull"
        )]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
            Account logined = await services.Auth.LoginAsync(loginModel);
            if (logined is null)
            {
                return Unauthorized("Username or password is wrong");
            }

            string token = await services.Auth.GenerateJwtAsync(logined, loginModel.RememberMe);
            return base.Ok(new {token=token, Id = logined.Id, Username=logined.Username, Email=logined.Email, Role=logined.Role.Name});
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.GoogleId}] Login for student with googel id token. Return JWT Token",
            Description = "Login for student with googel id token in Header. Return JWT Token if successfull"
        )]
        [CustomGoogleIdTokenAuthFilter]
        [HttpPost("Login/Google/Id-Token")]
        public async Task<IActionResult> LoginWithGoogleIdTokenAsync(bool rememberMe=true)
        {
            //var idToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = HttpContext.GetGoogleIdToken();
            Account logined = await services.Auth.LoginWithGoogle(idToken);
            if (logined is null)
            {
                return Unauthorized("Username or password is wrong");
            }
            string token = await services.Auth.GenerateJwtAsync(logined, rememberMe);
            return Ok(new { token = token, Id = logined.Id, Username = logined.Username, Email = logined.Email, Role = logined.Role.Name });
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.GoogleAccess}] Login for student with googel access token. Return JWT Token",
           Description = "Login for student with googel access token in Header. Return JWT Token if successfull"
       )]
        [CustomGoogleIdTokenAuthFilter]
        [HttpPost("Login/Google/Access-Token")]
        public async Task<IActionResult> LoginWithGoogleAccessTokenAsync(bool rememberMe = true)
        {
            return BadRequest("API Not Finneshed");
            //var idToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = HttpContext.GetGoogleIdToken();
            Account logined = await services.Auth.LoginWithGoogle(idToken);
            if (logined is null)
            {
                return Unauthorized("Username or password is wrong");
            }
            return Ok(await services.Auth.GenerateJwtAsync(logined, rememberMe));
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}] Register for student with form",
            Description = "Register for student with form"
        )]
        [HttpPost("Register/Student")]
        public async Task<IActionResult> StudentRegister(StudentRegisterDto dto)
        {
            ValidatorResult valResult = await validators.Accounts.ValidateParams(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            Account register = mapper.Map<Account>(dto);
            register.Password = register.Password.CustomHash();
            if (dto.IsStudent)
            {
                await services.Auth.Register(register, RoleNameEnum.Student);
            }
            else
            {
                await services.Auth.Register(register, RoleNameEnum.Parent);
            }
            return Ok();
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Guest}/{Finnished.True}] Register for parent with form",
            Description = "Register for parent with form"
        )]
        [HttpPost("Register/Parent")]
        public async Task<IActionResult> ParentRegister(ParentRegisterDto dto)
        {
            ValidatorResult valResult = await validators.Accounts.ValidateParams(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            Account register = mapper.Map<Account>(dto);
            await services.Auth.Register(register, RoleNameEnum.Student);
            return Ok();
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.False}] Register for student with form",
           Description = "Register for student with form"
       )]
        [HttpPost("Register/Student/Google")]
        public async Task<IActionResult> StudentRegisterWithGgAccessToken()
        {
            //if (dto.Password != dto.ConfirmPassword)
            //{
            //    return BadRequest("Xác nhận password không thành công");
            //}
            //Account register = mapper.Map<Account>(dto);
            //await services.Auth.Register(register, RoleNameEnum.Student);
            //return Ok(await services.Accounts.GetAccountByUserNameAsync(dto.Username));
            return BadRequest();
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Guest}/{Finnished.True}] Register for parent with form",
            Description = "Register for parent with form"
        )]
        [HttpPost("Register/Parent/Google")]
        public async Task<IActionResult> ParentRegisterWithGgAccessToken()
        {
            //if (dto.Password != dto.ConfirmPassword)
            //{
            //    return BadRequest("Xác nhận password không thành công");
            //}
            //Account register = mapper.Map<Account>(dto);
            //await services.Auth.Register(register, RoleNameEnum.Student);
            //return Ok(await services.Accounts.GetAccountByUserNameAsync(dto.Username));
            return BadRequest();
        }
        /// ////////////////////////////////////////////////////////////////////////////////////////
        [Tags(Actor.Test)]        
        [SwaggerOperation(
           Summary = $"[{Actor.Test}/{Finnished.True}] Get all the token sent in the header of the swagger request"
       )]
        [HttpGet("TestAuth/Tokens")]
        public async Task<IActionResult> GetTokens()
        {
            var accerssToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var headerToken = HttpContext.Request.Headers.Where(x => x.Key == "Authorization").Select(x=>x.Value);
            return Ok(new
            {
                Access_Token = accerssToken,
                Id_Token = idToken,
                Header_Token = headerToken,
            });
        }

        [Tags(Actor.Test)]        
        [SwaggerOperation(
          Summary = $"[{Actor.Test}/{Finnished.True}] Get all the data of the account of the swagger request"
      )]
        [Authorize]
        [HttpGet("TestAuth/LoginData")]
        public async Task<IActionResult> GetRoleData()
        {
            string? roles1 = String.Empty;
            foreach (var roleClaim in User.Claims.Where(role => role.Type.Contains("role", StringComparison.CurrentCultureIgnoreCase)))
            {
                roles1 += $" {roleClaim.Value}";
            }
            string emails = String.Empty;
            IEnumerable<Claim> claims = User.Claims;
            IEnumerable<Claim> emailClaims = claims.Where<Claim>(role => role.Type.Contains("mail", StringComparison.CurrentCultureIgnoreCase));
            foreach (var emailClaim in emailClaims)
            {
                emails += $" {emailClaim.Value}";
            }
            return Ok(new
            {
                roles = string.IsNullOrEmpty(roles1) ? "No role" : roles1,
                emails = string.IsNullOrEmpty(emails) ? "No mail" : emails
            });
        }

        [Tags(Actor.Test)]        
        [SwaggerOperation(
         Summary = $"[{Actor.Test}/{Finnished.True}] Test if the account of the swagger request is a student"
        )]
        [Authorize(Roles = "Student")]
        [HttpGet("TestAuth/Student")]
        public async Task<IActionResult> GetStudentData()
        {
            return Ok("You're student ");
        }

        [Tags(Actor.Test)]        
        [SwaggerOperation(
         Summary = $"[{Actor.Test}/{Finnished.True}] Test if the account of the swagger request is a student"
        )]
        [Authorize(Roles = "Parent")]
        [HttpGet("TestAuth/Parent")]
        public async Task<IActionResult> GetUserData()
        {
            return Ok("You're a parent ");
        }
    }
}
