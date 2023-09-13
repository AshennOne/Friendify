using API.Data;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsForUser(string username);
        Task AddPost(Post post);
        Task DeletePost(int id);
        Task<bool> SaveChangesAsync();
        Task EditPost(int id,Post post);
    }
}