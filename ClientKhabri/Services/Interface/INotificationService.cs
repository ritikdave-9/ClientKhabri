using System.Collections.Generic;
using System.Threading.Tasks;
using ClientKhabri.Dtos;

namespace ClientKhabri.Services.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotificationsAsync();
    }
}
