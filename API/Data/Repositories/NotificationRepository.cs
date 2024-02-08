using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the INotificationRepository interface and provides methods to interact with notifications in the database.
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        /// <summary>
        /// Represents the application's database context used for interacting with the underlying database.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class with the specified ApplicationDbContext.
        /// </summary>
        /// <param name="dbContext"></param>
        public NotificationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        /// <summary>
        /// Adds a new notification to the database.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type"></param>
        /// <param name="elementId"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the added Notification object.</returns>
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
        /// <summary>
        /// Retrieves a notification by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the Notification object with the specified ID.</returns>
        public async Task<Notification> GetNotificationById(int id)
        {
            return await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }
        /// <summary>
        /// Retrieves notifications for a specific user from the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns a collection of Notification objects representing the notifications for the specified user.</returns>

        public async Task<IEnumerable<Notification>> GetNotifications(User user)
        {
            var notifs = await _dbContext.Notifications.Where(n => n.ToUserId == user.Id && n.FromUserId != user.Id).OrderByDescending(n => n.CreateDate).ToListAsync();
            return notifs;
        }
        /// <summary>
        /// Removes a notification from the database based on its type and element ID.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="elementId"></param>
        /// <returns>Returns a task representing the asynchronous operation.</returns>

        public async Task RemoveNotification(NotiType type, int elementId)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Type == type && n.ElementId == elementId);
            if(notification != null)
            _dbContext.Notifications.Remove(notification);
            await _dbContext.SaveChangesAsync();
        }
    }
}