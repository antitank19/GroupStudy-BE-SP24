using Microsoft.AspNetCore.Http;
using MimeKit;

namespace ServiceLayer.ClassImplement
{
    public class MailMessageEntity
    {
        public MailMessageEntity()
        {
        }

        public MailMessageEntity(IEnumerable<string> receivers, string subject, string content,
            IFormFileCollection? attachments)
        {
            Receivers = new List<MailboxAddress>();

            Receivers.AddRange(receivers.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments?.ToList();
        }

        public MailMessageEntity(IEnumerable<string> receivers, string subject, string content,
            List<IFormFile>? attachments)
        {
            Receivers = new List<MailboxAddress>();

            Receivers.AddRange(receivers.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments ?? new List<IFormFile>();
        }

        public List<MailboxAddress> Receivers { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        //public IFormFileCollection Attachments { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
