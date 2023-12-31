using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ServiceLayer.Interface;
using Utilities.ServiceExtensions.Scheduler.Lib;

namespace APIExtension.Scheduler.Jobs;

public class StartMonthlyJob :  IJob
{
    public static readonly string schedule = CronScheduleExpression.StartMonthly;
    //public static string schedule => CronScheduleExpression.StartMonthly;
    //private readonly IInvoiceService invoiceService;
    //private readonly ICustomeMailService mailService;
    private readonly IAutoMailService mailService;
   

    public StartMonthlyJob(IServiceProvider service)
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
            new[] { "trankhaiminhkhoi@gmail.com", "trankhaiminhkhoi10a3@gmail.com" },
            "Test Start Monthly Job Scheduler",
            $"Email sent at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}",
            null
        );
        try
        {
            //await invoiceService.AutoFinishInvoice();
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