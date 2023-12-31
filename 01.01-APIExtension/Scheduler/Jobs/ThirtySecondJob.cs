using Microsoft.Extensions.DependencyInjection;
using Quartz;
using ServiceLayer.Interface;
using Utilities.ServiceExtensions.Scheduler.Lib;

namespace APIExtension.Scheduler.Jobs;

internal class ThirtySecondJob :  IJob
{
    public static readonly string schedule = CronScheduleExpression.ThirtySecond;
    //private readonly IInvoiceService invoiceService;
    //private readonly ICustomeMailService mailService;
    private readonly IAutoMailService mailService;

    public ThirtySecondJob(IServiceProvider service)
    {
        var scope = service.CreateScope();
        //mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
        //invoiceService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Invoices;
        //this.mailService = scope.ServiceProvider.GetRequiredService<IAutoMailService>();
        this.mailService = scope.ServiceProvider.GetRequiredService<IServiceWrapper>().Mails;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            //await invoiceService.AutoGenerateEmptyInvoice(CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        //await mailService.SendEmailWithDefaultTemplateAsync(
        //    new[] { "trankhaiminhkhoi@gmail.com", "trankhaiminhkhoi10a3@gmail.com", "tramdbse140878@fpt.edu.vn" },
        //    "Test Thirty Second Scheduler",
        //    $"Email sent at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}",
        //    null
        //);
        //await mailService.SendMonthlyStatAsync();
        Console.WriteLine($"\n\nThirty Second Task completed at {DateTime.Now.ToString("dd/MM/yy hh.mm.ss")}");
        //return Task.CompletedTask;
    }
}