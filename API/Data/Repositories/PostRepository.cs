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
        public async Task<IEnumerable<PostDto>> GetRepostedPosts(User user)
        {
            var postsIds = _dbContext.Posts.Where(p => p.AuthorId == user.Id && p.RepostedFromId != 0).Select(p => p.RepostedFromId);

            var OriginalPosts = _mapper.Map<IEnumerable<PostDto>>(await _dbContext.Posts.Where(p => postsIds.Contains(p.Id)).ToListAsync());



            return OriginalPosts;

        }
        public async Task<IEnumerable<PostDto>> SearchPosts(string searchstring)
        {
            var posts = await _dbContext.Posts.Where(post => post.TextContent.Contains(searchstring)).Include(p => p.Author).ToListAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _dbContext.Posts.Include(u => u.Author).Include(p => p.OriginalAuthor).Include(u => u.Likes).Include(u => u.Comments).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        public async Task UnRepost(Post post, User user)
        {
            var userPost = await _dbContext.Posts.FirstOrDefaultAsync(u => u.RepostedFromId == post.Id && u.AuthorId == user.Id);
            post.RepostCount -= 1;
            _dbContext.Posts.Remove(userPost);
        }
        public async Task<Post> GetPostById(int id)
        {
            return await _dbContext.Posts.Include(p => p.Author).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == id);
        }

        public IEnumerable<PostDto> GetPostsForUser(string username)
        {
            var posts = _dbContext.Posts.Include(p => p.OriginalAuthor).Where(u => u.Author.UserName.ToLower() == username.ToLower()).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
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