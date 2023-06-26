using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;



namespace stock_quote_alert
{
    internal class Mail
    {
        public void sendMail(MailConf conf,string recommendation,string emailBody)
        {
            MailAddress to = new MailAddress(conf.ToAddress.ToString());
            MailAddress from = new MailAddress(conf.FromAddress.ToString());
            MailMessage email = new MailMessage(from,to);
            email.Subject = recommendation;
            email.Body = emailBody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = conf.SmtpHost;
            smtpClient.Port = conf.SmtpPort;
            smtpClient.Credentials = new NetworkCredential(conf.SmtpUsername, conf.SmtpPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            try
            {
                smtpClient.Send(email);
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Error sending the email.\n");
                Console.WriteLine(e.Message);
            }

        }
    }


}
