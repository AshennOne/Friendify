using API.Entities;

namespace API.Interfaces
{
    public interface IPostLikeRepository
    {
        Task<IEnumerable<Post>> GetLikedPosts(string userId);
        Task<bool> AddLike(string userId, int postId);
        Task<bool> RemoveLike(string userId, int likeId);
        Task<bool> SaveChangesAsync();
    }
}