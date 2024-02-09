using API.Entities;

namespace API.Interfaces
{
    /// /// <summary>
    /// Interface for managing post likes.
    /// </summary>
    public interface IPostLikeRepository
    {
        /// <summary>
        /// Retrieves posts liked by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose liked posts are to be retrieved.</param>
        /// <returns>An asynchronous task returning an enumerable collection of liked posts.</returns>
        Task<IEnumerable<Post>> GetLikedPosts(string userId);
        /// <summary>
        /// Adds a like to a post by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user adding the like.</param>
        /// <param name="postId">The ID of the post to like.</param>
        /// <returns>An asynchronous task returning the added post like.</returns>
        Task<PostLike> AddLike(string userId, int postId);
        /// <summary>
        /// Removes a like from a post by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user removing the like.</param>
        /// <param name="postId">The ID of the post to remove the like from.</param>
        /// <returns>An asynchronous task returning the removed post like.</returns>
        Task<PostLike> RemoveLike(string userId, int postId);
        /// <summary>
        /// Checks if a post belongs to a specific user.
        /// </summary>
        /// <param name="user">The user to check against.</param>
        /// <param name="postId">The ID of the post to check ownership for.</param>
        /// <returns>An asynchronous task returning a boolean indicating whether the post belongs to the user.</returns>
        Task<bool> PostBelongsToUser(User user,int postId);
    }
}