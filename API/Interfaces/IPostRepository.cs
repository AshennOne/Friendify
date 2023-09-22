using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<PostDto> GetPostsForUser(string username);
        IEnumerable<PostDto> GetAllPosts();
        Task<Post> GetPostById(int id); 
        Task AddPost(Post post);
        Task DeletePost(int id);
        Task<bool> SaveChangesAsync();
        Task UnRepost(Post post);
        Task EditPost(int id, Post post);
        bool CheckIsReposted(Post post, string userId);
    }
}