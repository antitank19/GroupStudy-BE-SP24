using DataLayer.DBContext;
using DataLayer.DBObject;
using DataLayer.DbSeeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ImMemorySeeding
{
    public static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedInMemoryDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<GroupStudyContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return app;
        }
        public class DbInitializer
        {
            internal static void Initialize(GroupStudyContext context)
            {
                ArgumentNullException.ThrowIfNull(context, nameof(context));
                //context.Database.EnsureCreated();
                #region seed Role
                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(DbSeed.Roles);
                }
                #endregion

                #region seed Account
                if (!context.Accounts.Any())
                {

                    context.Accounts.AddRange(DbSeed.Accounts);

                }
                #endregion

                #region seed Supervision
                if (!context.Supervisions.Any())
                {

                    context.Supervisions.AddRange(DbSeed.Supervisions);

                }
                #endregion


                #region seed class
                if (!context.Classes.Any())
                {

                    context.Classes.AddRange(DbSeed.Classes);
                }
                #endregion

                #region seed subject
                if (!context.Subjects.Any())
                {
                    context.Subjects.AddRange(DbSeed.Subjects);
                }
                #endregion

                #region seed group
                if (!context.Groups.Any())
                {
                    context.Groups.AddRange(DbSeed.Groups);
                }
                #endregion

                #region seed group member
                if (!context.GroupMembers.Any())
                {
                    context.GroupMembers.AddRange(DbSeed.GroupMembers);
                }
                #endregion

                #region seed group subject
                if (!context.GroupSubjects.Any())
                {
                    context.GroupSubjects.AddRange(DbSeed.GroupSubjects);
                }
                #endregion

                #region seed invite
                if (!context.Invites.Any())
                {
                    context.Invites.AddRange(DbSeed.Invites);
                }
                #endregion

                #region seed request
                if (!context.Requests.Any())
                {
                    context.Requests.AddRange(DbSeed.Requests);
                }
                #endregion

                #region seed meeting
                if (!context.Meetings.Any())
                {
                    context.Meetings.AddRange(DbSeed.Meetings);
                }
                #endregion

                #region seed Connection
                if (!context.Connections.Any())
                {
                    context.Connections.AddRange(DbSeed.Connections);
                }
                #endregion

                #region seed Review
                if (!context.Reviews.Any())
                {
                    context.Reviews.AddRange(DbSeed.Reviews);
                }
                #endregion

                #region seed ReviewDetail
                if (!context.ReviewDetails.Any())
                {
                    context.ReviewDetails.AddRange(DbSeed.ReviewDetails);
                }
                #endregion

                #region seed Chat
                if (!context.Chats.Any())
                {
                    context.Chats.AddRange(DbSeed.Chats);
                }
                #endregion
                context.SaveChanges();
            }
        }
    }
}
