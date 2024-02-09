namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for creating a new message.
    /// </summary>
    public class CreateMessageDto
    {
        /// <summary>
        /// Gets or sets the ID of the user to whom the message will be sent.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content { get; set; }
    }
}