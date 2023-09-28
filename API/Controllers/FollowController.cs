using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class FollowController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IFollowRepository _followRepository;
        public FollowController(UserManager<User> userManager, IFollowRepository followRepository)
        {
            _followRepository = followRepository;
            _userManager = userManager;

        }
        [HttpGet("followers/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowersForUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _followRepository.GetFollowersForUser(id);
            return Ok(follows);
        }
        [HttpGet("followed/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowedByUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _followRepository.GetFollowedByUser(id);
            return Ok(follows);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<FollowDto>> Follow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot follow yourself");
            var follow = await _followRepository.GetFollow(user.Id, id);

            if (follow == null) follow = await _followRepository.Follow(user.Id, id);
            else return BadRequest("User already followed");
            if (await _followRepository.SaveChangesAsync())
                return Ok(follow);
            else return BadRequest("Following failed");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unfollow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot unfollow yourself");
            await _followRepository.Unfollow(user.Id, id);
            return NoContent();
        }
        private async Task<User> getUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}