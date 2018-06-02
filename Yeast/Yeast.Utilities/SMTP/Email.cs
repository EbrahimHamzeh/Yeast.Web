using System.Text;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace Yeast.Utilities.SMTP
{
    public class Email
    {
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;
        public Email(string smtpAddress, string email, string emailPassword, int smptpPort = 25, bool ssl = false)
        {
            _mailMessage = new MailMessage();
            _mailMessage.BodyEncoding = Encoding.UTF8;
            _mailMessage.HeadersEncoding = Encoding.UTF8;
            _mailMessage.SubjectEncoding = Encoding.UTF8;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.IsBodyHtml = true;

            _smtpClient = new SmtpClient(smtpAddress, smptpPort);
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(email, emailPassword);
            _smtpClient.EnableSsl = ssl;
        }

        public bool Send(string fromMail, List<string> toMails, string subject, string body)
        {
            return Send(fromMail, toMails, subject, body, null);
        }

        public bool Send(string fromMail, string toMail, string subject, string body)
        {
            return Send(fromMail,new List<string> { toMail }, subject, body, null);
        }

        public bool Send(string fromMail, List<string> toMails, string subject, string body, params string[] attachments)
        {
            foreach (var item in attachments)
            {
                _mailMessage.Attachments.Add(new Attachment(item));
            }
            _mailMessage.From = new MailAddress(fromMail);
            _mailMessage.Subject = subject;
            _mailMessage.Body = body;
            foreach (var item in toMails)
            {
                _mailMessage.To.Add(item);
            }
            _smtpClient.Send(_mailMessage);
            return true;
        }
    }
}
