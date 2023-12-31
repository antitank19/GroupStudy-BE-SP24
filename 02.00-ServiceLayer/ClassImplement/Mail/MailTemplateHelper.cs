namespace ServiceLayer.ClassImplement
{
    public static class MailTemplateHelper
    {
        public static string FOLDER = "MailTemplates";

        public static readonly string DEFAULT_TEMPLATE_FILE = "MailTemplate.html";
        public static readonly string MONTHLY_STAT_TEMPLATE_FILE = "MonthlyStatTemplate.html";
        public static readonly string CONFIRM_RESET_PASSWORD_TEMPLATE_FILE = "ResetPassTemplate.html";
        public static readonly string NEW_PASSWORD_TEMPLATE_FILE = "NewPassTemplate.html";

        public static string DEFAULT_TEMPLATE(string rootPath)
        {
            if (string.IsNullOrEmpty(default_template))
            {
                default_template = GetTemplate(rootPath + Path.DirectorySeparatorChar + FOLDER +
                                               Path.DirectorySeparatorChar + DEFAULT_TEMPLATE_FILE);
            }
            return default_template;
        }
        /// <summary>
        /// {0} is fullname <br/>
        /// {1} is month <br/>
        /// {2} is studentFullname<br/>
        /// {3} is studentUsername     <br/>
        /// {4} is total  <br/>
        /// {5} is attended <br/>
        /// {6} is missed  <br/>
        /// {7} is time <br/>
        /// {8} is average <br/>
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static string MONTHLY_STAT_TEMPLATE(string rootPath)
        {
            if (string.IsNullOrEmpty(monthly_stat_template))
            {
                monthly_stat_template = GetTemplate(rootPath + Path.DirectorySeparatorChar + FOLDER +
                                               Path.DirectorySeparatorChar + MONTHLY_STAT_TEMPLATE_FILE);
            }
            return monthly_stat_template;
        }
        /// <summary>
        /// {0} is logo <br/>
        /// {1} is fullname <br/>
        /// {2} is link
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static string CONFIRM_RESET_PASSWORD_TEMPLATE(string rootPath)
        {
            if (string.IsNullOrEmpty(confirm_reset_password_template))
            {
                confirm_reset_password_template = GetTemplate(rootPath + Path.DirectorySeparatorChar + FOLDER +
                                               Path.DirectorySeparatorChar + CONFIRM_RESET_PASSWORD_TEMPLATE_FILE);
            }
            return confirm_reset_password_template;
        }

        public static string NEW_PASSWORD_TEMPLATE(string rootPath)
        {
            if (string.IsNullOrEmpty(new_password_template))
            {
                new_password_template = GetTemplate(rootPath + Path.DirectorySeparatorChar + FOLDER +
                                               Path.DirectorySeparatorChar + NEW_PASSWORD_TEMPLATE_FILE);
            }
            return new_password_template;
        }

        public static string GetTemplate(string templatePath)
        {
            string template;
            try
            {
                using (var streamReader = File.OpenText(templatePath))
                {
                    template = streamReader.ReadToEnd();
                }
            }
            catch
            {
                Console.WriteLine($"Get Email Template {templatePath}: Not found");
                using (var streamReader = File.OpenText(DEFAULT_TEMPLATE_FILE))
                {
                    template = streamReader.ReadToEnd();
                }
            }

            return template;
        }

        //public static string ToHtmlTable(this Invoice invoice)
        //{
        //    var tables = "";
        //    var serviceTable = new StringBuilder("");
        //    serviceTable.Append("<table class=\"service-table\">");
        //    serviceTable.Append("<thead>");
        //    serviceTable.Append("<th>Service</th>");
        //    serviceTable.Append("<th>Fee</th>");
        //    serviceTable.Append("</thead>");
        //    serviceTable.Append("<tbody>");

        //    var requesttable = new StringBuilder("");
        //    requesttable.Append("<table class=\"service-table\">");
        //    requesttable.Append("<thead>");
        //    requesttable.Append("<th>Ticket</th>");
        //    requesttable.Append("<th>Fee</th>");
        //    requesttable.Append("</thead>");
        //    requesttable.Append("<tbody>");

        //    double serviceTotal = 0;
        //    double requestTotal = 0;
        //    var hasService = false;
        //    var hasRequest = false;
        //    foreach (var detail in invoice.InvoiceDetails)
        //    {
        //        if (detail.Service != null)
        //        {
        //            serviceTable.Append("<tr>");
        //            serviceTable.Append($"<td>{detail.Service.Name}</td >");
        //            serviceTable.Append($"<td>{detail.Amount}</td >"); ;

        //            serviceTable.Append("</tr>");
        //            serviceTotal += detail.Amount;
        //            hasService = true;
        //        }

        //        if (detail.Ticket != null)
        //        {
        //            requesttable.Append("<tr>");
        //            requesttable.Append($"<td>{detail.Ticket.TicketName}</td >");
        //            requesttable.Append($"<td>{detail.Amount}</td >");

        //            requesttable.Append("</tr>");
        //            requestTotal += detail.Amount;
        //            hasRequest = true;
        //        }
        //    }

        //    if (hasService)
        //    {
        //        serviceTable.Append("<tr>");
        //        serviceTable.Append("<td>Total</td >");
        //        serviceTable.Append($"<td>{serviceTotal}</td >");
        //        serviceTable.Append("</tr>");
        //        serviceTable.Append("</tbody>");
        //        serviceTable.Append("</table>");
        //        tables += serviceTable.ToString();
        //    }

        //    if (hasRequest)
        //    {
        //        requesttable.Append("<tr>");
        //        requesttable.Append("<td>Total</td >");
        //        requesttable.Append($"<td>{requestTotal}</td >");
        //        requesttable.Append("</tr>");
        //        requesttable.Append("</tbody>");
        //        requesttable.Append("</table>");
        //        tables += requesttable.ToString();
        //    }
        //    tables += $"Total: {requestTotal + serviceTotal + invoice.Amount}";

        //    return tables;
        //}

        #region field

        public static string default_template;
        public static string confirm_reset_password_template;
        public static string new_password_template;
        public static string monthly_stat_template;
        //public static string payment_reminder_template;
        //private static string payment_confirm_template;

        #endregion
    }
}
