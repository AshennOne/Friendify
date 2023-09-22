using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public PostRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }

        public async Task AddPost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
        }

        public async Task DeletePost(int id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(u => u.Id == id);
            var posts = await _dbContext.Posts.Where(p => p.RepostedFromId == id).ToListAsync();
            if (post != null)
                _dbContext.Posts.Remove(post);
            _dbContext.Posts.RemoveRange(posts);

        }

        public async Task EditPost(int id, Post post)
        {
            var oldPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            oldPost.ImgUrl = post.ImgUrl;
            oldPost.TextContent = post.TextContent;
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _dbContext.Posts.Include(u => u.Author).Include(u => u.Likes).Include(u => u.Comments).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        public async Task UnRepost(Post post)
        {
            var OriginalPost = await _dbContext.Posts.FirstOrDefaultAsync(u => u.Id == post.RepostedFromId);
            OriginalPost.RepostCount -= 1;
            _dbContext.Posts.Remove(post);
        }
        public async Task<Post> GetPostById(int id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<PostDto> GetPostsForUser(string username)
        {
            var posts = _dbContext.Posts.Where(u => u.Author.UserName.ToLower() == username.ToLower()).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public bool CheckIsReposted(Post post, string userId)
        {
            var isReposted = false;
            var posts = _dbContext.Posts.Where(p => p.AuthorId == userId);
            foreach (var userPost in posts)
            {
                if (userPost.RepostedFromId == post.Id)
                {
                    isReposted = true;
                }
            }
            return isReposted;
        }
    }
}