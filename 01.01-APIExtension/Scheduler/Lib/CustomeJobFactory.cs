using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Utilities.ServiceExtensions.Scheduler.Lib;

public class CustomeJobFactory : IJobFactory
{
    private readonly IServiceProvider service;

    public CustomeJobFactory(IServiceProvider service)
    {
        this.service = service;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return service.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        //Console.WriteLine($"CustomeJobFactory : IJobFactory ReturnJob() at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}");
    }
}