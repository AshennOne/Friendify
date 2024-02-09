namespace API.Entities
{
    /// <summary>
    /// Represents a like on a post in the application.
    /// </summary>
    public class PostLike
    {
        /// <summary>
        /// Unique identifier for the post like.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The ID of the user who liked the post.
        /// </summary>
        public string LikedById { get; set; }
        /// <summary>
        ///The user who liked the post.
        /// </summary>
        public User LikedBy { get; set; }
        /// <summary>
        /// The ID of the post that was liked.
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The post that was liked.
        /// </summary>
        public Post Post{get;set;}

    }
}