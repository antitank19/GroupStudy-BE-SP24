using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace APIExtension.Auth
{
    public static class JwtAuthExtension
    {
        private const string SecurityId = "Jwt Bearer";

        public static IServiceCollection AddJwtAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                #region google id token
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                #endregion
            })
            .AddJwtBearer(o =>
            {
                #region google id token
                //o.IncludeErrorDetails = true;
                //o.SecurityTokenValidators.Clear();
                //o.SecurityTokenValidators.Add(new GoogleTokenValidator());
                #endregion
                #region 2 phase auth
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    // Remember to set to true on production
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Authentication:JwtToken:TokenKey"])),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Authentication:JwtToken:Issuer"],
                    ValidAudience = configuration["Authentication:JwtToken:Audience"]
                };
                #region auth event (removed)
                //o.Events = new JwtBearerEvents
                //{
                //    OnChallenge = async context =>
                //    {
                //        // Call this to skip the default logic and avoid using the default response
                //        context.HandleResponse();

                //        var httpContext = context.HttpContext;
                //        const int statusCode = StatusCodes.Status401Unauthorized;

                //        var routeData = httpContext.GetRouteData();
                //        var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

                //        var factory = httpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                //        var problemDetails = factory.CreateProblemDetails(httpContext, statusCode);

                //        var result = new ObjectResult(problemDetails) { StatusCode = statusCode };
                //        await result.ExecuteResultAsync(actionContext);
                //    }
                //};
                #endregion
                #endregion
            });
            return services;
        }
        public static SwaggerGenOptions AddJwtAuthUi(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(SecurityId, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,  //ko xài .ApiKey vì .http sẽ ko cần gõ
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization using the Bearer scheme. \"Bearer\" is not needed.Just paste the jwt"
            }
                );
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
                        new string[]{}
                    }
                }
            );
            return options;
        }
    }
}
