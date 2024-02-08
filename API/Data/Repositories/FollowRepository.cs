using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the IFollowRepository interface and provides methods to interact with user follow relationships in the database.
    /// </summary>
    public class FollowRepository : IFollowRepository
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
        /// Initializes a new instance of the <see cref="FollowRepository"/> class with the specified ApplicationDbContext and IMapper.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public FollowRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        /// <summary>
        /// Adds a new follow relationship between two users in the database.
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="followedId"></param>
        /// <returns>Returns a Task representing the asynchronous operation.</returns>
        public async Task Follow(string followerId, string followedId)
        {
            var follow = new Follow
            {
                FollowedId = followedId,
                FollowerId = followerId
            };
            await _dbContext.Follows.AddAsync(follow);
        }
        /// <summary>
        /// Retrieves a list of users followed by a specific user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a collection of FollowDto objects representing the users followed by the specified user.</returns>
        public IEnumerable<FollowDto> GetFollowedByUser(string id)
        {
            var follows = _dbContext.Follows.Where(p => p.FollowerId == id);
            return _mapper.Map<IEnumerable<FollowDto>>(follows);
        }
        /// <summary>
        /// Retrieves a list of users who are following a specific user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a collection of FollowDto objects representing the followers of the specified user.</returns>
        public IEnumerable<FollowDto> GetFollowersForUser(string id)
        {
            var follows = _dbContext.Follows.Where(p => p.FollowedId == id);
            return _mapper.Map<IEnumerable<FollowDto>>(follows);
        }
        /// <summary>
        /// Retrieves a follow relationship between two users.
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="followedId"></param>
        /// <returns> Returns a Task representing the asynchronous operation, which resolves to a FollowDto object representing the follow relationship between the two users.</returns>
        public async Task<FollowDto> GetFollow(string followerId, string followedId)
        {
            var follow = await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
            return _mapper.Map<FollowDto>(follow);
        }
        /// <summary>
        /// Removes a follow relationship between two users.
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="followedId"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to the removed Follow object representing the follow relationship.</returns>
        public async Task<Follow> Unfollow(string followerId, string followedId)
        {
            var follow = await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);

            if (follow != null)
                _dbContext.Follows.Remove(follow);
            return follow;

        }

    }
}