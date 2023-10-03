using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NotificationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Notification> AddNotification(User from, User to, NotiType type, int elementId)
        {
            var notification = new Notification
            {
                FromUserId = from.Id,
                ToUserId = to.Id,
                ImgUrl = from.ImgUrl,
                FromUserName = from.UserName,
                Type = type,
                ElementId = elementId
            };
            switch (type)
            {
                case NotiType.PostLike:
                    notification.Message = "liked your post";
                    break;

                case NotiType.Follow:
                    notification.Message = "started following you";
                    break;

                case NotiType.Comment:
                    notification.Message = "commented your post";
                    break;

                case NotiType.Repost:
                    notification.Message = "reposted your post";
                    break;

                default:
                    break;
            }
            
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetNotifications(User user)
        {
            var notifs = await _dbContext.Notifications.Where(n => n.ToUserId == user.Id && n.FromUserId != user.Id).ToListAsync();
            return notifs;
        }

        public async Task RemoveNotification(NotiType type, int elementId)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Type == type && n.ElementId == elementId);
            if(notification != null)
            _dbContext.Notifications.Remove(notification);
            await _dbContext.SaveChangesAsync();
        }
    }
}