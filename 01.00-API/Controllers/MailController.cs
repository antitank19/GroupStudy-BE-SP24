using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        //private readonly IMapper _mapper;
        //private readonly IWebHostEnvironment env;
        private readonly IServiceWrapper services;

        //private readonly IWebHostEnvironment env;
        public MailController(/*IAutoMailService emailSender,*/ /*IWebHostEnvironment env,*/ IMapper mapper,
            IServiceWrapper serviceWrapper)
        {
            //mailService = emailSender;
            //this.env = env;
            //_mapper = mapper;
            this.services = serviceWrapper;
        }


        //[HttpGet("paymentReminder")]
        //public async Task<IActionResult> SendPaymentReminder()
        //{
        //    //string rootPath = env.WebRootPath;
        //    var result = await mailService.SendPaymentReminderAsync();

        //    return result ? Ok("Send mail successfully") : BadRequest("Somthing went wrong");
        //}

        [HttpPost]
        public async Task<IActionResult> PostManyReceiversWithTemplate([FromForm] List<string> receivers,
            [FromForm] string subject,
            [FromForm] string content, [FromForm] IFormFileCollection attachments)
        {
            //string rootPath = env.WebRootPath;
            //MailMessageEntity mail = new MailMessageEntity( receivers, subject, content, attachments);
            //var result = await mailService.SendEmailWithDefaultTemplateAsync(mail, rootPath);
            var result =
                await services.Mails.SendEmailWithDefaultTemplateAsync(receivers, subject, content,
                    attachments /*, rootPath*/);

            return result ? Ok("Send mail successfully") : BadRequest("Somthing went wrong");
        }

        #region Unused Code

        //[HttpGet]
        //public async Task<IActionResult> GetWithTemplate(string receiver, string subject, string content)
        //{
        //    //string rootPath = env.WebRootPath;
        //    var result = await serviceWrapper.Mails.SendEmailWithDefaultTemplateAsync(new string[] { receiver }, subject, content, attachments: null/*, rootPath*/);

        //    return result ? Ok("Send mail successfully") : BadRequest("Somthing went wrong");
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetWithTemplate(string receiver, string subject, string content)
        //{
        //    var rootPath = env.WebRootPath;
        //    var result =
        //        await mailService.SendEmailWithDefaultTemplateAsync(new[] { receiver }, subject, content, null, rootPath);

        //    return result ? Ok("Send mail successfully") : BadRequest("Somthing went wrong");
        //}

        //[HttpPost]
        //public async Task<IActionResult> PostManyReceiversWithTemplate([FromForm] List<string> receivers,
        //    [FromForm] string subject,
        //    [FromForm] string content, [FromForm] IFormFileCollection attachments)
        //{
        //    var rootPath = env.WebRootPath;
        //    //MailMessageEntity mail = new MailMessageEntity( receivers, subject, content, attachments);
        //    //var result = await mailService.SendEmailWithDefaultTemplateAsync(mail, rootPath);
        //    var result =
        //        await mailService.SendEmailWithDefaultTemplateAsync(receivers, subject, content, attachments, rootPath);

        //    return result ? Ok("Send mail successfully") : BadRequest("Somthing went wrong");
        //}

        #endregion
    }
}
