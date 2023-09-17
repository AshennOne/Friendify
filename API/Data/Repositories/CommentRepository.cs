using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        public async Task AddComment(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        public async Task<bool> BelongsToUser(string userId, int commentId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null) return false;
            if (comment.CommentedById == userId) return true;
            return false;
        }

        public async Task DeleteComment(int commentId, string userId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.CommentedById == userId);
            if (comment != null)
                _dbContext.Comments.Remove(comment);
        }

        public async Task EditComment(int commentId, string userId, string content)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.CommentedById == userId);
            if (comment != null)
                comment.Content = content;


        }

        public IEnumerable<CommentResponseDto> GetCommentsForPost(int postId)
        {
            var comments =  _dbContext.Comments.Include(c => c.CommentedBy).Include(c => c.Post).ThenInclude(p => p.Author).Where(c => c.PostId == postId);
            return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);


        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}