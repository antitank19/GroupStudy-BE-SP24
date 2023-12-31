using APIExtension.Scheduler.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Utilities.ServiceExtensions.Scheduler.Jobs;
using Utilities.ServiceExtensions.Scheduler.Lib;

namespace Utilities.ServiceExtensions.Scheduler;

public static class SchedulerService
{
    public static IServiceCollection AddSchedulerService(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddHostedService<QuarztHostedService>();
        services.AddSingleton<IJobFactory, CustomeJobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        //if (environment.IsDevelopment())
        if (environment.IsProduction() || environment.IsDevelopment())
        {
            services.AddSingleton<DailyJob>();
            services.AddSingleton<WeeklyJob>();
            services.AddSingleton<EndMonthlyJob>();
            services.AddSingleton(new ScheduledJob(typeof(DailyJob), DailyJob.schedule));
            services.AddSingleton(new ScheduledJob(typeof(WeeklyJob), WeeklyJob.schedule));
            services.AddSingleton(new ScheduledJob(typeof(EndMonthlyJob), EndMonthlyJob.schedule));
        }
        //easy testing
        if (environment.IsDevelopment())
        {
            services.AddSingleton<ThirtySecondJob>();
            services.AddSingleton(new ScheduledJob(typeof(ThirtySecondJob), ThirtySecondJob.schedule));
        }
        return services;
    }
}