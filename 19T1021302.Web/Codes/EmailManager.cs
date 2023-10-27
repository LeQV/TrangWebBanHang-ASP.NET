using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace _19T1021302.Web.Codes
{
    public class EmailManager
    {
        public static void AppSettings(out string UserID, out string Password, out string SMTPPort, out string Host)
        {
            UserID = ConfigurationManager.AppSettings.Get("UserID");
            Password = ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            Host = ConfigurationManager.AppSettings.Get("Host");
        }
        public static bool SendEmail(string From, string Subject, string Body, string To, string UserName, string Password, string SMTPPort, string Host)
        {
			try
			{
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(To);
                mailMessage.From = new MailAddress(From);
                mailMessage.Subject = Subject;
                mailMessage.Body = ("Your Username is: " + UserName + "<br/>" + "Your Password is:" + Password+"<br/> Please");
                mailMessage.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = Host;
                System.Net.NetworkCredential ntwd = new System.Net.NetworkCredential();
                ntwd.UserName = UserName; //Your Email ID  
                ntwd.Password = "drtbtpnacyfemzeb"; // Your Password  
                smt.UseDefaultCredentials = false;
                smt.Credentials = ntwd;
                smt.Port = Convert.ToInt32(SMTPPort);
                smt.EnableSsl = true;
                smt.Send(mailMessage);
            }catch(Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
			}
            return true;
        }
    }
}