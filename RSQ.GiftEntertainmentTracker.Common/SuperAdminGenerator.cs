using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace RSQ.GiftEntertainmentTracker.Common
{
    public class SuperAdminGenerator
    {
        public static void Mail(string emailId, string url)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress("muthuganesh.rsquare@gmail.com");
            mail.To.Add(emailId);
            mail.Subject = "Super Admin Url Generator";
            mail.Body = "The Gererator Url is:" + url.ToString().Trim();

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("muthuganesh.rsquare@gmail.com", "rsquaremuthu");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
