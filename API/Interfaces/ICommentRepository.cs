using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Interface for interacting with comment data.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Retrieves comments for a post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A collection of comment response DTOs.</returns>
        IEnumerable<CommentResponseDto> GetCommentsForPost(int postId);
        /// <summary>
        /// Edits a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to edit.</param>
        /// <param name="userId">The ID of the user performing the edit.</param>
        /// <param name="content">The new content of the comment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task EditComment(int commentId, string userId,string content);
        /// <summary>
        /// Adds a new comment.
        /// </summary>
        /// <param name="comment">The comment to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddComment(Comment comment);
        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <param name="userId">The ID of the user performing the deletion.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteComment(int commentId, string userId);
        /// <summary>
        /// Checks if a comment belongs to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>True if the comment belongs to the user, false otherwise.</returns>
        Task<bool> BelongsToUser(string userId, int commentId);
        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>The comment.</returns>
        Task<Comment> GetCommentById(int id);
    }
}