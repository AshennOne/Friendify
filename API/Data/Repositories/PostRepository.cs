using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the IPostRepository interface and provides methods to interact with posts in the database.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        /// <summary>
        /// Represents the application's database context used for interacting with the underlying database.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Represents an instance of IMapper used for object mapping between different types within the application.
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class with the specified ApplicationDbContext and IMapper.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public PostRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        /// <summary>
        /// Adds a new post to the database.
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Represents the asynchronous operation of adding a post.</returns>
        public async Task AddPost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
        }
        /// <summary>
        /// Deletes a post from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Represents the asynchronous operation of deleting a post.</returns>
        public async Task DeletePost(int id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(u => u.Id == id);
            var posts = await _dbContext.Posts.Where(p => p.RepostedFromId == id).ToListAsync();
            if (post != null)
                _dbContext.Posts.Remove(post);
            _dbContext.Posts.RemoveRange(posts);

        }
        /// <summary>
        /// Edits an existing post in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns>Represents the asynchronous operation of editing a post.</returns>
        public async Task EditPost(int id, Post post)
        {
            var oldPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            oldPost.ImgUrl = post.ImgUrl;
            oldPost.TextContent = post.TextContent;
        }
        /// <summary>
        /// Retrieves posts that have been reposted by a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Represents the asynchronous operation of retrieving reposted posts.</returns>
        public async Task<IEnumerable<PostDto>> GetRepostedPosts(User user)
        {
            var postsIds = _dbContext.Posts.Where(p => p.AuthorId == user.Id && p.RepostedFromId != 0).Select(p => p.RepostedFromId);

            var OriginalPosts = _mapper.Map<IEnumerable<PostDto>>(await _dbContext.Posts.Where(p => postsIds.Contains(p.Id)).ToListAsync());



            return OriginalPosts;

        }
        /// <summary>
        ///  Searches for posts containing a specific search string in their text content.
        /// </summary>
        /// <param name="searchstring"></param>
        /// <returns>Represents the asynchronous operation of searching posts.</returns>
        public async Task<IEnumerable<PostDto>> SearchPosts(string searchstring)
        {
            var posts = await _dbContext.Posts.Where(post => post.TextContent.Contains(searchstring)).Include(p => p.Author).ToListAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        /// <summary>
        /// Retrieves all posts from the database.
        /// </summary>
        /// <returns>Collection of all posts retrieved from the database.</returns>
        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _dbContext.Posts.Include(u => u.Author).Include(p => p.OriginalAuthor).Include(u => u.Likes).Include(u => u.Comments).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        /// <summary>
        /// Removes a repost of a post made by a user.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="user"></param>
        /// <returns>Represents the asynchronous operation of unreposting a post.</returns>
        public async Task<Post> UnRepost(Post post, User user)
        {
            var userPost = await _dbContext.Posts.FirstOrDefaultAsync(u => u.RepostedFromId == post.Id && u.AuthorId == user.Id);
            post.RepostCount -= 1;
            _dbContext.Posts.Remove(userPost);
            return userPost;
        }
        /// <summary>
        /// Retrieves a post from the database by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Represents the asynchronous operation of retrieving a post by its ID.</returns>
        public async Task<Post> GetPostById(int id)
        {
            return await _dbContext.Posts.Include(p => p.Author).Include(p => p.Comments).Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == id);

        }
        /// <summary>
        /// Converts a Post entity object to a PostDto data transfer object.
        /// </summary>
        /// <param name="Post"></param>
        /// <returns>The converted PostDto object.</returns>
        public PostDto ConvertToDto(Post Post)
        {
            return  _mapper.Map<PostDto>(Post);
        }
        /// <summary>
        /// Retrieves posts authored by a specific user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Collection of posts authored by the specified user.</returns>
        public IEnumerable<PostDto> GetPostsForUser(string username)
        {
            var posts = _dbContext.Posts.Include(u => u.Author).Include(p => p.OriginalAuthor).Include(u => u.Likes).Include(u => u.Comments).Where(u => u.Author.UserName.ToLower() == username.ToLower()).OrderByDescending(u => u.Created);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }
        /// <summary>
        /// Checks if a post has been reposted by a specific user.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="userId"></param>
        /// <returns>Bool that indicates whether the post has been reposted by the specified user.</returns>
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