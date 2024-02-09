using System.Text.Json.Serialization;

namespace API.Entities
{
    /// <summary>
    /// This class represents a notification entity in the application.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the unique identifier of the notification.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who triggered the notification.
        /// </summary>
        public string FromUserId { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user to whom the notification is directed.
        /// </summary>
        [JsonIgnore]
        public string ToUserId { get; set; }
        /// <summary>
        /// Gets or sets a boolean value indicating whether the notification has been read.
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the notification was created. Defaults to the current UTC time.
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        ///  Gets or sets the username of the user who triggered the notification.
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the type of notification (e.g., PostLike, Follow, Comment, Repost).
        /// </summary>
        public NotiType Type { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the element associated with the notification (e.g., PostId, UserId).
        /// </summary>
        public int ElementId { get; set; }
        /// <summary>
        /// Gets or sets the URL of the image associated with the notification.
        /// </summary>
        public string ImgUrl { get; set; }

    }
}