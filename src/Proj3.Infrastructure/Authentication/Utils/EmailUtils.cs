using Proj3.Application.Common.Interfaces.Utils.Authentication;
using System.Net;
using System.Net.Mail;

namespace Proj3.Infrastructure.Authentication.Utils
{
    public class EmailUtils : IEmailUtils
    {
        private const string applicationEmail = "johanna.keeling@ethereal.email";

        public Task<bool> SendEmail(string receiver,string subject, string content)
        {
            try
            {
                MailMessage message = new MailMessage(new MailAddress(applicationEmail), new MailAddress(receiver));
                message.Body = content;                

                string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                message.Body += Environment.NewLine + someArrows;
                message.BodyEncoding = System.Text.Encoding.UTF8;

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                using (var smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(applicationEmail, "ZuM6g5XczJgEHCBQE7");

                    string userState = "test message1";
                    smtp.SendAsync(message, userState);
                    return Task.FromResult(true);
                }
            }    
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }        
    }
}
