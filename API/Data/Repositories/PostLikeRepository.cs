using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Data.Repositories
{

    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PostLikeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task AddLike(string userId, int postId)
        {
            if (await _dbContext.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.LikedById == userId) == null)
            {
                await _dbContext.AddAsync(new PostLike
                {
                    LikedById = userId,
                    PostId = postId
                });
            }


        }

        public async Task<bool> PostBelongsToUser(User user, int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return false;
            if (post.AuthorId == user.Id) return true;
            return false;
        }


        public async Task<IEnumerable<Post>> GetLikedPosts(string userId)
        {
            return await _dbContext.Likes.Include(p => p.Post).Where(u => u.LikedById == userId).Select(p => p.Post).ToListAsync();
        }

        public async Task RemoveLike(string userId, int postId)
        {
            var postLike = await _dbContext.Likes.FirstOrDefaultAsync(u => u.PostId == postId && u.LikedById == userId);
            if(postLike != null)
            _dbContext.Likes.Remove(postLike);

        }
      
    }
}