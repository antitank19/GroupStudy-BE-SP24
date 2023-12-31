using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.HttpContext
{
    public static class HttpContextExtension
    {
        public static string GetEmailFromJwtToken(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                throw new Exception("Token missing");
            }
            string? token = context.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Replace("Bearer ", "");
            if (!_tokenHandler.CanReadToken(token))
            {
                throw new Exception("Invalidated token");
            }


            var payload = GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()).Result;
            return payload.Email;

        }
        public static string GetGoogleIdToken(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                throw new Exception("Token missing");
            }
            string? token = context.Request.Headers.First(x => x.Key == "Authorization").Value;
            token = token.Replace("Bearer ", "");
            if (!_tokenHandler.CanReadToken(token))
            {
                throw new Exception("Invalidated token");
            }


            return token;

        }

    }
}
