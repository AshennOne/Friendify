using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the ICommentRepository interface and provides methods to interact with comments in the database.
    /// </summary>
    public class CommentRepository : ICommentRepository
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
        /// Initializes a new instance of the <see cref="CommentRepository"/> class with the specified ApplicationDbContext and IMapper.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Returns a Task representing the asynchronous operation.</returns>
        public async Task AddComment(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }
        /// <summary>
        /// Checks if a comment belongs to a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="commentId"></param>
        /// <returns>Returns true if the comment belongs to the user; otherwise, returns false.</returns>
        public async Task<bool> BelongsToUser(string userId, int commentId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null) return false;
            if (comment.CommentedById == userId) return true;
            return false;
        }
        /// <summary>
        /// Deletes a comment from the database.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <returns>Returns a Task representing the asynchronous operation.</returns>
        public async Task DeleteComment(int commentId, string userId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.CommentedById == userId);
            if (comment != null)
                _dbContext.Comments.Remove(comment);
        }
        /// <summary>
        /// Edits the content of a comment in the database.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="userId"></param>
        /// <param name="content"></param>
        /// <returns>Returns a Task representing the asynchronous operation.</returns>
        public async Task EditComment(int commentId, string userId, string content)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.CommentedById == userId);
            if (comment != null)
                comment.Content = content;


        }
        /// <summary>
        /// Retrieves comments for a specific post from the database.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Returns a collection of CommentResponseDto objects representing the comments for the specified post.</returns>

        public IEnumerable<CommentResponseDto> GetCommentsForPost(int postId)
        {
            var comments = _dbContext.Comments.Include(c => c.CommentedBy).Include(c => c.Post).ThenInclude(p => p.Author).Where(c => c.PostId == postId).OrderByDescending(c => c.Created);
            return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);


        }
        /// <summary>
        /// Retrieves a comment by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the Comment object with the specified ID.</returns>
        public async Task<Comment> GetCommentById(int id){
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}