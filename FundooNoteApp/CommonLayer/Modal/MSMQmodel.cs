using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Modal
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void sendData2Queue(string Token)
        {
            messageQueue.Path = @".\private$\Token";

            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }


            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string subject = "Fundoo Notes Reset Link";
            string Body = Token;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("firstuser311@gmail.com");
            mail.To.Add("firstuser311@gmail.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "<body><p>Dear User,<br><br>" +
                "Forgot your Password?<br>" +
                "Click on the below link to reset your Password.<br></body>" + Token;

            mail.Body = htmlBody;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("firstuser311@gmail.com", "swoxpsvtboyndwcb"),
                EnableSsl = true
            };
            SMTP.Send(mail);
            messageQueue.BeginReceive();
        }
    }
}
