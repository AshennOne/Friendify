using API.Interfaces;
using AutoMapper;

namespace API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public UnitOfWork(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        public IPostRepository PostRepository => new PostRepository(_dbContext,_mapper);

        public IPostLikeRepository PostLikeRepository =>new PostLikeRepository(_dbContext);

        public IFollowRepository FollowRepository => new FollowRepository(_dbContext,_mapper);

        public ICommentRepository CommentRepository => new CommentRepository(_dbContext, _mapper);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}