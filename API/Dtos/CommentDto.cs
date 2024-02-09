namespace API.Dtos
{
    /// <summary>
    ///  This class represents a data transfer object (DTO) for comments.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// Gets or sets the ID of the post to which the comment belongs.
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content {get;set;}
    }
}