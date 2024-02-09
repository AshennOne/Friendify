namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for messages.
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the message sender.
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// Gets or sets the DTO representing the sender of the message.
        /// </summary>
        public UserClientDto Sender { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the message receiver.
        /// </summary>
        public string ReceiverId { get; set; }
        /// <summary>
        /// Gets or sets the DTO representing the receiver of the message.
        /// </summary>
        public UserClientDto Receiver { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the message has been read.
        /// </summary>
        public bool Read { get; set; } = false;
        /// <summary>
        /// Gets or sets the date and time when the message was sent (in UTC).
        /// </summary>
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Gets or sets the date and time when the message was read (if applicable).
        /// </summary>
        public DateTime ReadDate { get; set; }
        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content { get; set; }
    }
}