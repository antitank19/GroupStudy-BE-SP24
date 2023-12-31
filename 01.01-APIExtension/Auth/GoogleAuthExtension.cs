using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace APIExtension.Auth
{
    public static class GoogleAuthExtension
    {
        private const string SecurityId = "Google Bearer";
        public static SwaggerGenOptions AddGoogleAuthUi(this SwaggerGenOptions options, IConfiguration configuration)
        {
            options.AddSecurityDefinition(SecurityId, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri(configuration["Authentication:Google:Web:auth_uri"]??"https://accounts.google.com/o/oauth2/v2/auth"),
                        Scopes = new Dictionary<string, string> {
                    //{ "openid", "Allow this app to get some basic account info" },
                    { "email", "email" },
                    { "profile", "profile" } ,
                    { "https://www.googleapis.com/auth/userinfo.email", "email2" } ,
                    { "https://www.googleapis.com/auth/userinfo.profile", "profile2" }

                },
                         
                        TokenUrl = new Uri(configuration["Authentication:Google:Web:token_uri"])
                    }
                },
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    {"x-tokenName", new OpenApiString("id_token")}
                },
            });     

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecurityId
                        }
                    },
                    new List<string> {/*"openid",*/ "email", "profile", "https://www.googleapis.com/auth/userinfo.email", "https://www.googleapis.com/auth/userinfo.profile" }
                }                                               
            });
            return options;
        }
        public static SwaggerUIOptions SetGoogleAuthUi(this SwaggerUIOptions options, IConfiguration configuration)
        {
            #region google access token
            //options.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
            //options.OAuthClientId(configuration["Authentication:Google:Web:client_id"]);
            //options.OAuthClientSecret(configuration["Authentication:Google:Web:client_secret"]);


            //options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            //options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "openid", "anyNonceStringHere" } });
            #endregion
            #region google access token
            options.OAuthClientId(configuration["Authentication:Google:Web:client_id"]);
    options.OAuthClientSecret(configuration["Authentication:Google:Web:client_secret"]);
            options.InjectJavascript("/Swaggers/googleSwaggerUi.js");
            #endregion
            return options;
        }
    }
}
