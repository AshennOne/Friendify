using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task AddPost(Post post)
        {
           await _dbContext.Posts.AddAsync(post);
        }

        public async Task DeletePost(int id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(u => u.Id==id);
             _dbContext.Posts.Remove(post);
        }

        public async Task EditPost(int id,Post post)
        {
            var oldPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            oldPost.ImgUrl = post.ImgUrl;
            oldPost.TextContent = post.TextContent;
        }

        public async Task<IEnumerable<Post>> GetPostsForUser(string username)
        {
           return await _dbContext.Posts.Where(u => u.Author.UserName.ToLower() == username.ToLower()).ToListAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() >0;
        }
    }
}