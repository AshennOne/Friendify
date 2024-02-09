namespace API.Entities
{
    /// <summary>
    /// This class represents a message entity in the application.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who sent the message.
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// Gets or sets the user who sent the message.
        /// </summary>
        public User Sender { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who received the message.
        /// </summary>
        public string ReceiverId { get; set; }
        /// <summary>
        /// Gets or sets the user who received the message.
        /// </summary>
        public User Receiver { get; set; }
        /// <summary>
        /// Gets or sets a boolean value indicating whether the message has been read. Defaults to false.
        /// </summary>
        public bool Read { get; set; } = false;
        /// <summary>
        /// Gets or sets the date and time when the message was sent. Defaults to the current UTC time.
        /// </summary>
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Gets or sets the date and time when the message was read.
        /// </summary>
        public DateTime ReadDate { get; set; }
        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content{get;set;}
    }
}