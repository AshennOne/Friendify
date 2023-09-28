using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IFollowRepository
    {
        IEnumerable<FollowDto> GetFollowersForUser(string id);
        IEnumerable<FollowDto> GetFollowedByUser(string id);
        Task<FollowDto> Follow(string followerId, string followedId);
        Task Unfollow(string followerId, string followedId);
        Task<bool> SaveChangesAsync();
        Task<FollowDto> GetFollow(string followerId, string followedId);
    }
}