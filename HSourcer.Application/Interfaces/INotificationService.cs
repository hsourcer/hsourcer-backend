using HSourcer.Application.Notifications.Models;
using System.Threading.Tasks;

namespace HSourcer.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);

    }
}

