using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the IPostLikeRepository interface and provides methods to interact with post likes in the database.
    /// </summary>
    public class PostLikeRepository : IPostLikeRepository
    {
        /// <summary>
        /// Represents the application's database context used for interacting with the underlying database.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="PostLikeRepository"/> class with the specified ApplicationDbContext.
        /// </summary>
        /// <param name="dbContext"></param>
        public PostLikeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        /// <summary>
        /// Adds a like to a post.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="postId"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the added PostLike object if the like is successfully added, or null if the like already exists.</returns>
        public async Task<PostLike> AddLike(string userId, int postId)
        {
            if (await _dbContext.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.LikedById == userId) == null)
            {
                var like = new PostLike
                {
                    LikedById = userId,
                    PostId = postId
                };
                await _dbContext.AddAsync(like);
                return like;
            }
            return null;

        }
        /// <summary>
        /// Checks if a post belongs to a specific user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="postId"></param>
        /// <returns> Returns a Task representing the asynchronous operation, which resolves to true if the post belongs to the user, or false otherwise.</returns>
        public async Task<bool> PostBelongsToUser(User user, int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null) return false;
            if (post.AuthorId == user.Id) return true;
            return false;
        }

        /// <summary>
        /// Retrieves posts liked by a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns a collection of Post objects representing the posts liked by the specified user.</returns>
        public async Task<IEnumerable<Post>> GetLikedPosts(string userId)
        {
            return await _dbContext.Likes.Include(p => p.Post).Where(u => u.LikedById == userId).Select(p => p.Post).ToListAsync();
        }
        /// <summary>
        /// Removes a like from a post.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="postId"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the removed PostLike object if the like is successfully removed, or null if the like doesn't exist.</returns>
        public async Task<PostLike> RemoveLike(string userId, int postId)
        {
            var postLike = await _dbContext.Likes.FirstOrDefaultAsync(u => u.PostId == postId && u.LikedById == userId);
            if (postLike != null)
                _dbContext.Likes.Remove(postLike);
            return postLike;
        }

        
    }
}