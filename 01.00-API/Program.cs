using API;
using API.SignalRHub;
using API.SignalRHub.Tracker;
using APIExtension.Auth;
using DataLayer.ImMemorySeeding;
using APIExtension.Validator;
using DataLayer.DBContext;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using RepositoryLayer.ClassImplement;
using RepositoryLayer.Interface;
using ServiceLayer.ClassImplement;
using ServiceLayer.Interface;
using System.Text.Json;
using System.Text.Json.Serialization;
using Utilities.ServiceExtensions.Scheduler;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
bool IsInMemory = configuration["ConnectionStrings:InMemory"].ToLower() == "true";

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region dbContext
builder.Services.AddDbContext<GroupStudyContext>(options =>
{
    options.EnableSensitiveDataLogging();
    //options.EnableRetryOnFailure
    if (IsInMemory)
    {
        options.UseInMemoryDatabase("GroupStudy");
    }
    else
    {
        options.UseSqlServer(configuration.GetConnectionString("Default"));
    }
});
//Use for scaffolding api controller. remove later
builder.Services.AddDbContext<TempContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
    //options.UseInMemoryDatabase("GroupStudy");
});
#endregion

#region cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("signalr",builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(hostName => true));

});
#endregion

builder.Services.AddDirectoryBrowser();

#region SignalR
builder.Services.AddSignalR();
#endregion
#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region service and repo
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddScoped<IRepoWrapper, RepoWrapper>();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
//builder.Services.AddScoped<IAutoMailService, AutoMailService>();
#endregion

builder.Services.AddSchedulerService(builder.Environment);

#region validator
builder.Services.AddScoped<IValidatorWrapper, ValidatorWrapper>();
#endregion

builder.Services.AddJwtAuthService(configuration);
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
    #region auth
    options.AddJwtAuthUi();
    options.AddGoogleAuthUi(configuration);
    #endregion
});


var app = builder.Build();

if (IsInMemory)
{
    Console.WriteLine("+++++++++++++++++++++++++++++++++++++InMemory+++++++++++++++++++++++++++++++++++");
    app.SeedInMemoryDb();
}
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    #region setGoogle loginPage
    options.SetGoogleAuthUi(configuration);
    #endregion
});
app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "UploadFile")),
    RequestPath = "/uploadfile",
    EnableDirectoryBrowsing = true
});

app.UseCors("signalr");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<GroupHub>("hubs/grouphub");
app.MapHub<DrawHub>("hubs/drawhub");
app.MapHub<MeetingHub>("hubs/meetinghub");

app.Run();
