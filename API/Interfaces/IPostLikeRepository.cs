using API.Entities;

namespace API.Interfaces
{
    public interface IPostLikeRepository
    {
        Task<IEnumerable<Post>> GetLikedPosts(string userId);
        Task AddLike(string userId, int postId);
        Task RemoveLike(string userId, int postId);
        Task<bool> SaveChangesAsync();
        Task<bool> PostBelongsToUser(User user,int postId);
    }
}