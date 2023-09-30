using API.Entities;

namespace API.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(User from, User to, NotiType type, int elementId);
        Task<IEnumerable<Notification>> GetNotifications(User user);
        Task RemoveNotification(NotiType type, int elementId);
    }
}