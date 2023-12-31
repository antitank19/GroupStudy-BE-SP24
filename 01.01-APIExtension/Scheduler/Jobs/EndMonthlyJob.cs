using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ServiceLayer.Interface;
using Utilities.ServiceExtensions.Scheduler.Lib;

namespace APIExtension.Scheduler.Jobs;

public class EndMonthlyJob : IJob
{
    public static readonly string schedule = CronScheduleExpression.EndMonthly;
    private readonly IAutoMailService mailService;

    public EndMonthlyJob(IServiceProvider service/*, IAutoMailService mailService*/)
    {
        var scope = service.CreateScope();
        //mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
        //invoiceService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Invoices;
        //this.mailService = scope.ServiceProvider.GetRequiredService<IAutoMailService>();
        this.mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
        //this.mailService = mailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await mailService.SendEmailWithDefaultTemplateAsync(
            new[] { "trankhaiminhkhoi10a3@gmail.com" },
            "Test End Monthly Job Scheduler",
            $"Email sent at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}",
            null
        );
        try
        {
            //await invoiceService.AutoGenerateEmptyInvoice(CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        //await mailService.SendPaymentReminderAsync(CancellationToken.None);
        Console.WriteLine($"Monthly Task completed at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}");
        //return Task.CompletedTask;
    }
}