using API.Entities;

namespace API.Interfaces
{
    public interface IPostLikeRepository
    {
        Task<IEnumerable<Post>> GetLikedPosts(string userId);
        Task<PostLike> AddLike(string userId, int postId);
        Task<PostLike> RemoveLike(string userId, int postId);
        Task<bool> PostBelongsToUser(User user,int postId);
    }
}