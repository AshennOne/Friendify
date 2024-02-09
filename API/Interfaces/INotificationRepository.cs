using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Interface for managing notifications.
    /// </summary>
    public interface INotificationRepository
    {
        /// <summary>
        /// Adds a new notification to the repository.
        /// </summary>
        /// <param name="from">The user who triggered the notification.</param>
        /// <param name="to">The user who will receive the notification.</param>
        /// <param name="type">The type of notification.</param>
        /// <param name="elementId">The ID of the element associated with the notification.</param>
        /// <returns>An asynchronous task returning the added notification.</returns>
        Task<Notification> AddNotification(User from, User to, NotiType type, int elementId);
        /// <summary>
        /// Retrieves notifications for a specific user.
        /// </summary>
        /// <param name="user">The user whose notifications are to be retrieved.</param>
        /// <returns>An asynchronous task returning an enumerable collection of notifications.</returns>
        Task<IEnumerable<Notification>> GetNotifications(User user);
        /// <summary>
        /// Removes a notification from the repository.
        /// </summary>
        /// <param name="type">The type of notification to remove.</param>
        /// <param name="elementId">The ID of the element associated with the notification to remove.</param>
        /// <returns>An asynchronous task.</returns>
        Task RemoveNotification(NotiType type, int elementId);
        /// <summary>
        /// Retrieves a notification by its ID.
        /// </summary>
        /// <param name="id">The ID of the notification to retrieve.</param>
        /// <returns>An asynchronous task returning the notification.</returns>
        Task<Notification> GetNotificationById(int id);
    }
}