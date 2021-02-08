using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.ExternalServices
{
    public class MailingService : IMailingService
    {
        private readonly IConfiguration _appConfig;
        public MailingService(IConfiguration appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task BroadcastEmailAsync(List<string> recipients, string subject, string body)
        {
            if (recipients.Count == 0) return;
            var email = PrepareEmail(
                recipients: recipients,
                subject: subject,
                body: body);

            using var smtp = new SmtpClient();
            
            smtp.Connect(
                host: _appConfig["Email:SmtpServer"], 
                port: 587, 
                options: SecureSocketOptions.StartTls);
            
            smtp.Authenticate(
                userName: _appConfig["Email:Login"],
                password: _appConfig["Email:Password"]);
            
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        private MimeMessage PrepareEmail(List<string> recipients, string subject, string body)
        {
            var email = new MimeMessage();
            
            email.From.Add(MailboxAddress.Parse(_appConfig["Email:Login"]));
            email.To.AddRange(recipients.Select(str => MailboxAddress.Parse(str)));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };
            
            return email;
        }
    }
}
