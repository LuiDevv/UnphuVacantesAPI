using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;

namespace api.interfaces
{
    public interface INotificationInterface
    {
       Task<IEnumerable<NotificationDTO>> GetAllNotifications();
        Task<NotificationDTO> GetNotificationById(int id);
        Task<NotificationDTO> CreateNotification(CreateNotificationRequest request);
        Task<bool> UpdateNotification(int id, UpdateNotificationRequest request);
        Task<bool> DeleteNotification(int id); 
    }
}