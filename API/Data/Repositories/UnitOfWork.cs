using API.Interfaces;
using AutoMapper;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the IUnitOfWork interface and provides repositories for interacting with different entities in the database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
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
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified ApplicationDbContext and IMapper.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public UnitOfWork(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        /// <summary>
        /// Gets the repository for interacting with post entities in the database.
        /// </summary>
        public IPostRepository PostRepository => new PostRepository(_dbContext,_mapper);
        /// <summary>
        /// Gets the repository for interacting with post like entities in the database.
        /// </summary>
        public IPostLikeRepository PostLikeRepository =>new PostLikeRepository(_dbContext);
        /// <summary>
        /// Gets the repository for interacting with follow entities in the database.
        /// </summary>
        public IFollowRepository FollowRepository => new FollowRepository(_dbContext,_mapper);
        /// <summary>
        /// Gets the repository for interacting with comment entities in the database.
        /// </summary>
        public ICommentRepository CommentRepository => new CommentRepository(_dbContext, _mapper);
        /// <summary>
        /// Gets the repository for interacting with notification entities in the database.
        /// </summary>
        public INotificationRepository NotificationRepository => new NotificationRepository(_dbContext);
        /// <summary>
        /// Gets the repository for interacting with message entities in the database.
        /// </summary>
        public IMessageRepository MessageRepository => new MessageRepository(_dbContext, _mapper);
        /// <summary>
        /// Saves changes made to the database asynchronously.
        /// </summary>
        /// <returns>Represents the asynchronous operation of saving changes to the database. Returns a boolean indicating whether any changes were saved.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}