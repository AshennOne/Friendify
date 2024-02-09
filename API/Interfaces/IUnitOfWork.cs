namespace API.Interfaces
{
    /// <summary>
    /// Interface for a unit of work pattern, which groups repositories and provides a way to save changes.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for posts.
        /// </summary>
        IPostRepository PostRepository { get; }
        /// <summary>
        /// Gets the repository for post likes.
        /// </summary>
        IPostLikeRepository PostLikeRepository { get; }
        /// <summary>
        /// Gets the repository for follows.
        /// </summary>
        IFollowRepository FollowRepository { get; }
        /// <summary>
        /// Gets the repository for messages.
        /// </summary>
        IMessageRepository MessageRepository { get; }
        /// <summary>
        /// Gets the repository for comments.
        /// </summary>
        ICommentRepository CommentRepository { get; }
        /// <summary>
        /// Gets the repository for notifications.
        /// </summary>
        INotificationRepository NotificationRepository { get; }
        /// <summary>
        /// Saves changes asynchronously to the underlying database.
        /// </summary>
        /// <returns>The task result that indicates whether the save operation was successful.</returns>
        Task<bool> SaveChangesAsync();
    }
}