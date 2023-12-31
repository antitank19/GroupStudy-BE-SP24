using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace Utilities.ServiceExtensions.Scheduler.Lib;

public class QuarztHostedService : IHostedService
{
    private readonly IJobFactory jobFactory;
    private readonly IEnumerable<ScheduledJob> scheduledJobs;
    private readonly ISchedulerFactory schedulerFactory;


    public QuarztHostedService(IJobFactory jobFactory, IEnumerable<ScheduledJob> scheduledJobs,
        ISchedulerFactory schedulerFactory)
    {
        Console.WriteLine("Scheduler Service Started");
        this.jobFactory = jobFactory;
        this.scheduledJobs = scheduledJobs;
        this.schedulerFactory = schedulerFactory;
    }

    public IScheduler Scheduler { get; set; }

    public async Task StartAsync(CancellationToken token)
    {
        Scheduler = await schedulerFactory.GetScheduler(token);
        Scheduler.JobFactory = jobFactory;
        foreach (var scheduledJob in scheduledJobs)
        {
            var job = CreateJob(scheduledJob);
            var trigger = CreateTrigger(scheduledJob);
            await Scheduler.ScheduleJob(job, trigger, token);
        }

        await Scheduler.Start(token);
    }

    public async Task StopAsync(CancellationToken token)
    {
        await Scheduler.Shutdown(token);
    }

    private static IJobDetail CreateJob(ScheduledJob scheduledJob)
    {
        var type = scheduledJob.Type;
        Console.WriteLine($"Create {type.Name} Job");
        return JobBuilder.Create(type)
            .WithIdentity(type.FullName ?? string.Empty)
            .WithDescription(type.Name)
            .Build();
    }

    private static ITrigger CreateTrigger(ScheduledJob scheduledJob)
    {
        var type = scheduledJob.Type;
        Console.WriteLine($"Create {type.Name} Trigger");
        return TriggerBuilder.Create().WithIdentity($"{type.FullName}.trigger")
            .WithCronSchedule(scheduledJob.ScheduleExpression)
            .WithDescription(scheduledJob.ScheduleExpression)
            .Build();
    }
}