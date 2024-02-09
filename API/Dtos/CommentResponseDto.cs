namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for comment 
    /// </summary>
    public class CommentResponseDto
    {
        /// <summary>
        /// Gets or sets the ID of the comment.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the user who made the comment, represented by a UserClientDto object.
        /// </summary>
        public UserClientDto CommentedBy { get; set; }
        /// <summary>
        /// Gets or sets the post to which the comment belongs, represented by a PostDto object.
        /// </summary>
        public PostDto Post { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the comment was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content {get;set;}
    }
}