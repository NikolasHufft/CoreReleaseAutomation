using System;
using System.Net.Mail;

namespace CoreReleaseAutomation.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(string releaseType, string version, string pathDestiny, string description, string emailSubject, 
                                    string emailBody, string emailFrom, string emailTo, string smtp)
        {
            try
            {
                using var client = new SmtpClient();

                var subject = emailSubject.Replace("XXXXXX", $"{releaseType}-{version}");
                var body = emailBody.Replace("XXXXXX", $"{releaseType}-{version}")
                        + Environment.NewLine + $"Path: {pathDestiny}"
                        + $"Description:{Environment.NewLine} {description}";

                client.Host = smtp;

                client.SendMailAsync(emailFrom, emailTo, subject, body);
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
