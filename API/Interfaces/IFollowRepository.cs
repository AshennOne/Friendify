using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Interface for managing user follow relationships.
    /// </summary>
    public interface IFollowRepository
    {
        /// <summary>
        /// Retrieves followers for a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An enumerable collection of followers.</returns>
        IEnumerable<FollowDto> GetFollowersForUser(string id);
        /// <summary>
        /// Retrieves users followed by a specific user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An enumerable collection of followed users.</returns>
        IEnumerable<FollowDto> GetFollowedByUser(string id);
        /// <summary>
        /// Follows a user.
        /// </summary>
        /// <param name="followerId">The ID of the follower.</param>
        /// <param name="followedId">The ID of the user being followed.</param>
        /// <returns>An asynchronous task.</returns>
        Task Follow(string followerId, string followedId);
        /// <summary>
        /// Unfollows a user.
        /// </summary>
        /// <param name="followerId">The ID of the follower.</param>
        /// <param name="followedId">The ID of the user being unfollowed.</param>
        /// <returns>An asynchronous task returning the unfollowed relationship.</returns>
        Task<Follow> Unfollow(string followerId, string followedId);
        /// <summary>
        /// Retrieves a follow relationship between two users.
        /// </summary>
        /// <param name="followerId">The ID of the follower.</param>
        /// <param name="followedId">The ID of the followed user.</param>
        /// <returns>An asynchronous task returning the follow relationship.</returns>
        Task<FollowDto> GetFollow(string followerId, string followedId);
    }
}