using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public class EmailSender
    {
        public static async Task<bool> MailSender(string userEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.To.Add(userEmail);
            message.From = new MailAddress("programista2020@onet.pl", "Aplikacja Ebok");
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "programista2020@onet.pl",
                    Password = "Juniordeveloper1"
                };

                smtp.UseDefaultCredentials = true;
                smtp.Credentials = credential;
                smtp.Host = "smtp.poczta.onet.pl";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                return true;

            }
        }
    }
}
