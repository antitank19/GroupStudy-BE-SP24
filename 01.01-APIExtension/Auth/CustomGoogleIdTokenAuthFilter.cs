using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace APIExtension.Auth
{
    public class CustomGoogleIdTokenAuthFilter : Attribute, IAuthorizationFilter
    {
        //private readonly string[] AcceptedRoles;
        private readonly JwtSecurityTokenHandler _tokenHandler;


        public CustomGoogleIdTokenAuthFilter(params string[] acceptedRoles)
        {
            //AcceptedRoles = acceptedRoles;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            string? token = context.HttpContext.GetTokenAsync("access_token").Result ?? context.HttpContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization").Value;

            bool isLogined = false;
            if (string.IsNullOrEmpty(token))
            {
                context.ModelState.AddModelError("Google token missing", "No google token found");
            }
            else
            {
                token = token.Replace("Bearer ", "");
                if (!_tokenHandler.CanReadToken(token))
                {
                    context.ModelState.AddModelError("Invalidated google token", "Your google token is invalidated");
                }
            }
            if (context.ModelState.IsValid)
            {
                try
                {
                    var payload = GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()).Result;
                    var email = payload.Email;
                    //check if email is registered
                    bool isRegistered = new List<String> { "trankhaiminhkhoi10a3@gmail.com", "trankhaiminhkhoigmail.com" }.Contains(email);
                    if (!isRegistered)
                    {
                        context.ModelState.AddModelError("Not registerd", "Would you like to registerd?");
                    }
                    else
                    {
                        isLogined = true;
                    }
                    #region unsused
                    //if (!claimPrinciple.IsInRole("Admin"))
                    //{
                    //    throw new Exception();
                    //}
                    //var roles = claimPrinciple.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                    //if(!roles.Any(role=>role == "Admin"))
                    //{
                    //    throw new Exception();
                    //}
                    #endregion
                }
                catch (Exception ex)
                {
                    context.ModelState.AddModelError("Auth exception", ex.Message);
                }
            }
            if (!context.ModelState.IsValid && !isLogined)
            {
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
    public static class GoogleIdTokenUltil
    {
        
    }
}
