using API.Entities;

namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for posts.
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the post.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the text content of the post.
        /// </summary>
        public string TextContent { get; set; }
        /// <summary>
        /// Gets or sets the URL of the image associated with the post (if any).
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Gets or sets the DTO representing the author of the post.
        /// </summary>
        public UserClientDto Author { get; set; }
        /// <summary>
        /// Gets or sets the DTO representing the original author of the post (in case of a repost).
        /// </summary>
        public UserClientDto OriginalAuthor { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the original author of the post (in case of a repost).
        /// </summary>
        public string OriginalAuthorId { get; set; }
        /// <summary>
        /// Gets or sets the number of likes received by the post.
        /// </summary>
        public int LikesCount { get; set; }
        /// <summary>
        /// Gets or sets the number of comments posted on the post.
        /// </summary>
        public int CommentsCount { get; set; }
        /// <summary>
        /// Gets or sets the number of times the post has been reposted.
        /// </summary>
        public int RepostCount { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the original post that was reposted (if applicable).
        /// </summary>
        public int RepostedFromId{get;set;}
    }
}