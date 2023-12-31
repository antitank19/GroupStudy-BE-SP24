namespace ServiceLayer.ClassImplement
{
    public class MailConfiguration
    {
        /// <summary>
        ///     Your identification seen by receiver
        ///     Recommend to be your email address to avoid getting marked as spam
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///     Mail service server
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        ///     Port of mail service server
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Your email address
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     For gmail
        ///     generrated password for less secured app to login to email account
        ///     see detail at https://support.google.com/mail/answer/7126229
        ///     see how to generate at https://support.google.com/accounts/answer/185833
        ///     For other mail services
        ///     Password when you login
        /// </summary>
        public string AppPassword { get; set; }
    }
}
