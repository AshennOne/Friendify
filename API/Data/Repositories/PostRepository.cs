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
            _dbContext.Posts.Remove(post);
        }

        public async Task EditPost(int id, Post post)
        {
            var oldPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            oldPost.ImgUrl = post.ImgUrl;
            oldPost.TextContent = post.TextContent;
        }

        public IEnumerable<PostDto> GetAllPosts(User user)
        {
            var posts = _dbContext.Posts.Where(u => u.AuthorId != user.Id).Include(u => u.Author).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
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
    }
}