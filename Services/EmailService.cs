using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Dicer.Interfaces;

namespace Dicer.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string html)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Constants.Constants.fromMail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(Constants.Constants.smtpServer, Constants.Constants.smtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(Constants.Constants.smtpLogin,Constants.Constants.smtpPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
