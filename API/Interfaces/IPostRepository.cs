using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<PostDto> GetPostsForUser(string username);
        IEnumerable<PostDto> GetAllPosts();
        Task AddPost(Post post);
        Task DeletePost(int id);
        Task<bool> SaveChangesAsync();
        Task EditPost(int id, Post post);
    }
}