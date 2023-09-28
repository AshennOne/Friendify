using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public FollowRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        public async Task<FollowDto> Follow(string followerId, string followedId)
        {
            var follow = new Follow
            {
                FollowedId = followedId,
                FollowerId = followerId
            };
            await _dbContext.Follows.AddAsync(follow);
            return _mapper.Map<FollowDto>(follow);
        }

        public IEnumerable<FollowDto> GetFollowedByUser(string id)
        {
            var follows = _dbContext.Follows.Where(p => p.FollowerId == id);
            return _mapper.Map<IEnumerable<FollowDto>>(follows);
        }

        public IEnumerable<FollowDto> GetFollowersForUser(string id)
        {
            var follows = _dbContext.Follows.Where(p => p.FollowedId == id);
            return _mapper.Map<IEnumerable<FollowDto>>(follows);
        }
        public async  Task<FollowDto> GetFollow(string followerId, string followedId)
        {
            var follow = await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
            return _mapper.Map<FollowDto>(follow);
        }
        public async Task Unfollow(string followerId, string followedId)
        {
            var follow = await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
           
                if(follow != null)
                _dbContext.Follows.Remove(follow);
        
                
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}