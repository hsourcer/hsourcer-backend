using HSourcer.Application.Notifications.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSourcer.Infrastructure.Helpers
{
    public static class EmailBuilder
    {
        public static SendGridMessage CreateMessage(Message message, EmailFrom senderInfo)
        {
            var gridMessage = new SendGridMessage();

            gridMessage.SetFrom(new EmailAddress(senderInfo.Email, senderInfo.Name));

            var recipients = new List<EmailAddress>();
            foreach(var recipient in message.To)
            {
                recipients.Add(new EmailAddress(recipient));
            }

            gridMessage.AddTos(recipients);

            gridMessage.SetSubject(message.Subject);

            gridMessage.AddContent(MimeType.Html, message.Body);
           //gridMessage.AddContent(MimeType.Html, "<p>Hello World!</p>");

            return gridMessage;
        }
    }
}
