using HSourcer.Application.Interfaces;
using HSourcer.Application.Notifications.Models;
using HSourcer.Infrastructure.Helpers;
using HSourcer.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace HSourcer.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public IOptions<MailConfig> MailConfigOptions { get; }
        public NotificationService(IOptions<MailConfig> options)
        {
            MailConfigOptions = options;
        }
        public async Task SendAsync(Message message)
        {
            var emailSender = new EmailSender(MailConfigOptions);
            await emailSender.SendMessage(message);
        }
    }
}