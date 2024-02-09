using System.Text.Json.Serialization;

namespace API.Entities
{
    /// <summary>
    /// Represents a post entity in the application.
    /// </summary>
    public class Post
    {
        /// <summary>
        ///  Unique identifier for the post.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The user who authored the post.
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// The ID of the original author of the post, if it is a repost.
        /// </summary>
        public string OriginalAuthorId { get; set; }
        /// <summary>
        /// The original author of the post, if it is a repost.
        /// </summary>
        public User OriginalAuthor { get; set; }
        /// <summary>
        ///  The ID of the author of the post.
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary>
        /// The textual content of the post.
        /// </summary>
        public string TextContent { get; set; }
        /// <summary>
        /// The URL of any image attached to the post.
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// The date and time when the post was created.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The list of likes received by the post.
        /// </summary>
        [JsonIgnore]
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
        /// <summary>
        /// The list of comments made on the post.
        /// </summary>
        [JsonIgnore]
        public List<Comment> Comments { get; set; } = new List<Comment>();
        /// <summary>
        /// The ID of the post that this post is reposted from, if it is a repost.
        /// </summary>
        public int RepostedFromId { get; set; }
        /// <summary>
        /// The count of reposts of this post.
        /// </summary>
        public int RepostCount{get;set;} = 0;

    }
}