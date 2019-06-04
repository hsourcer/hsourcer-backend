using HSourcer.Application.Notifications.Models;
using HSourcer.Infrastructure.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HSourcer.Infrastructure.Helpers
{
    public class EmailSender
    {
        public IOptions<MailConfig> MailConfigOptions { get; }
        public EmailSender(IOptions<MailConfig> options)
        {
            MailConfigOptions = options;
        }

        public async Task<Response> SendMessage(Message message)
        {
            var senderInfo = new EmailFrom
            {
                Email = MailConfigOptions.Value.FromEmail,
                Name = MailConfigOptions.Value.FromName
            };

            var apiKey = MailConfigOptions.Value.Key;
            var client = new SendGridClient(apiKey);
            var msg = EmailBuilder.CreateMessage(message, senderInfo);

            return await client.SendEmailAsync(msg);
        }
    }
}