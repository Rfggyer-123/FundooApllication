using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.model
{
    public class Sent
    {
        public string SendingMail(string emailTo, string token)
        {
            try
            {
                string emailFrom = "shivub9880@gmail.com"; // Replace with your Gmail address

                MailMessage message = new MailMessage(emailFrom, emailTo);

                string mailBody = "Token generated: " + token;
                message.Subject = "Generated token will expire after 1 hour";
                message.Body = mailBody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                // Replace with your Gmail credentials
                smtpClient.Credentials = new NetworkCredential("shivub9880@gmail.com", "gqre vorq oelg zqxz");

                smtpClient.Send(message);

                return emailFrom;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
