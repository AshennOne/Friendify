using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Interface for managing posts.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Retrieves posts for a specific user.
        /// </summary>
        /// <param name="username">The username of the user whose posts are to be retrieved.</param>
        /// <returns>An enumerable collection of post DTOs.</returns>
        IEnumerable<PostDto> GetPostsForUser(string username);
        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>An enumerable collection of all post DTOs.</returns>
        IEnumerable<PostDto> GetAllPosts();
        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>An asynchronous task returning the retrieved post.</returns>
        Task<Post> GetPostById(int id);
        /// <summary>
        /// Adds a new post.
        /// </summary>
        /// <param name="post">The post to add.</param>
        /// <returns>An asynchronous task representing the addition of the post.</returns>
        Task AddPost(Post post);
        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>An asynchronous task representing the deletion of the post.</returns>
        Task DeletePost(int id);
        /// <summary>
        /// Removes a repost from a post by a user.
        /// </summary>
        /// <param name="post">The post to remove the repost from.</param>
        /// <param name="user">The user who reposted the post.</param>
        /// <returns>An asynchronous task representing the removal of the repost.</returns>
        Task<Post> UnRepost(Post post, User user);
        /// <summary>
        /// Edits a post.
        /// </summary>
        /// <param name="id">The ID of the post to edit.</param>
        /// <param name="post">The updated post data.</param>
        /// <returns>An asynchronous task representing the editing of the post.</returns>
        Task EditPost(int id, Post post);
        /// <summary>
        /// Checks if a post is reposted by a specific user.
        /// </summary>
        /// <param name="post">The post to check for repost.</param>
        /// <param name="userId">The ID of the user to check repost status for.</param>
        /// <returns>A boolean indicating whether the post is reposted by the user.</returns>
        bool CheckIsReposted(Post post, string userId);
        /// <summary>
        /// Retrieves posts reposted by a specific user.
        /// </summary>
        /// <param name="user">The user whose reposted posts are to be retrieved.</param>
        /// <returns>An asynchronous task returning an enumerable collection of reposted post DTOs.</returns>
        Task<IEnumerable<PostDto>> GetRepostedPosts(User user);
        /// <summary>
        /// Searches posts by a specific search string.
        /// </summary>
        /// <param name="searchstring">The search string to filter posts.</param>
        /// <returns>An asynchronous task returning an enumerable collection of post DTOs matching the search string.</returns>
        Task<IEnumerable<PostDto>> SearchPosts(string searchstring);
        /// <summary>
        /// Converts a post entity to its corresponding DTO.
        /// </summary>
        /// <param name="Post">The post entity to convert.</param>
        /// <returns>The DTO representation of the post entity.</returns>
        PostDto ConvertToDto(Post Post);
    }   
}