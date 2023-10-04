using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<CommentResponseDto> GetCommentsForPost(int postId);
        Task EditComment(int commentId, string userId,string content);
        Task AddComment(Comment comment);
        Task DeleteComment(int commentId, string userId);
        Task<bool> BelongsToUser(string userId, int commentId);
        Task<Comment> GetCommentById(int id);
    }
}