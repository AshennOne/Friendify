namespace API.Entities
{
    /// <summary>
    /// This class represents a comment entity in the application.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique identifier of the comment.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who posted the comment.
        /// </summary>
        public string CommentedById { get; set; }
        /// <summary>
        /// Gets or sets the user who posted the comment.
        /// </summary>
        public User CommentedBy { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the post to which the comment belongs.
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// Gets or sets the post to which the comment belongs.
        /// </summary>
        public Post Post { get; set; }
        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the comment was created. Defaults to the current UTC time.
        /// </summary>
        public DateTime Created {get;set;} = DateTime.UtcNow;
    }
}