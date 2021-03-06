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
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        public NotificationService(IOptions<MailConfig> options, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            MailConfigOptions = options;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }
        public async Task SendAsync(Message message)
        {
            var emailSender = new EmailSender(MailConfigOptions);
            await emailSender.SendMessage(message);
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            return await _razorViewToStringRenderer.RenderViewToStringAsync(viewName, model);
        }
    }
}