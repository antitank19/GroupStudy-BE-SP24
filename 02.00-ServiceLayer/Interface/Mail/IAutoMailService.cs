using DataLayer.DBObject;
using Microsoft.AspNetCore.Http;
using ServiceLayer.ClassImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAutoMailService
    {
        public Task<bool> SendEmailWithDefaultTemplateAsync(IEnumerable<string> receivers, string subject, string content,
       IFormFileCollection attachments);

        public Task<bool> SendConfirmResetPasswordMailAsync(Account account, string serverLink);
        public Task<bool> SendNewPasswordMailAsync(Account account);
        public Task<bool> SendMonthlyStatAsync();

        //public Task<bool> SendPaymentReminderAsync();

        #region unsued code

        //public Task<bool> SendEmailWithDefaultTemplateAsync(MailMessageEntity mail);
        //Task<bool> SendSimpleMailAsync(IEnumerable<string> receivers, string subject, string content, IFormFileCollection attachments);

        #endregion
    }
}
