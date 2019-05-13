using HSourcer.Application.Interfaces;
using HSourcer.Application.Notifications.Models;
using System.Threading.Tasks;

namespace HSourcer.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}

