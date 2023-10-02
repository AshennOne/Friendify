using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IFollowRepository
    {
        IEnumerable<FollowDto> GetFollowersForUser(string id);
        IEnumerable<FollowDto> GetFollowedByUser(string id);
        Task Follow(string followerId, string followedId);
        Task<Follow> Unfollow(string followerId, string followedId);
        Task<FollowDto> GetFollow(string followerId, string followedId);
    }
}