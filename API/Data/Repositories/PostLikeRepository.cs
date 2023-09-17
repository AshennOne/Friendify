using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace API.Data.Repositories
{

    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PostLikeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<bool> AddLike(string userId, int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if(post.AuthorId == userId) return false;
            if(await _dbContext.Likes.FirstOrDefaultAsync(u => u.LikedById==userId && u.PostId == postId)!=null) return false;
            await _dbContext.AddAsync(new PostLike
            {
                LikedById = userId,
                PostId = postId
            });
            return true;
        }

        public async Task<IEnumerable<Post>> GetLikedPosts(string userId)
        {
            return await _dbContext.Likes.Include(p => p.Post).Where(u => u.LikedById == userId).Select(p => p.Post).ToListAsync();
        }

        public async Task<bool> RemoveLike(string userId,int likeId)
        {
            var postLike = await _dbContext.Likes.FirstOrDefaultAsync(u => u.Id == likeId && u.LikedById==userId);
            if(postLike == null) return false;
            _dbContext.Likes.Remove(postLike);
            return true;
        }
        public async Task<bool> SaveChangesAsync(){
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}