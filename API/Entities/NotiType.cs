namespace API.Entities
{
    /// <summary>
    /// This enum represents the types of notifications that can occur in the application.
    /// </summary>
    public enum NotiType
    {
        /// <summary>
        /// Type of notification about post like
        /// </summary>
        PostLike,
        /// <summary>
        /// Type of notification about follow
        /// </summary>
        Follow,
        /// <summary>
        /// Type of notification about comment
        /// </summary>
        Comment,
        /// <summary>
        /// Type of notification about repost
        /// </summary>
        Repost
    }
}