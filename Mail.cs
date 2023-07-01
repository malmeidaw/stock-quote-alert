using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;



namespace StockQuoteAlert
{
    internal class Mail
    {
        public bool sendMail(MailConf conf,string recommendation,string emailBody)
        {
            MailAddress to = new MailAddress(conf.toAddress.ToString());
            MailAddress from = new MailAddress(conf.fromAddress.ToString());
            MailMessage email = new MailMessage(from,to);
            email.Subject = recommendation;
            email.Body = emailBody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = conf.smtpHost;
            smtpClient.Port = conf.smtpPort;
            smtpClient.Credentials = new NetworkCredential(conf.smtpUsername, conf.smtpPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            try
            {
                smtpClient.Send(email);
                return true;
            }
            catch (SmtpException e)
            {
                Console.WriteLine("\nError sending the email.\n");
                Console.WriteLine(e.Message+"\n");
                return false;
            }

        }
    }
}
