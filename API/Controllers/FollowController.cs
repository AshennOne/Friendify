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
        private readonly IUnitOfWork _unitOfWork;
        public FollowController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        [HttpGet("followers/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowersForUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _unitOfWork.FollowRepository.GetFollowersForUser(id);
            return Ok(follows);
        }
        [HttpGet("followed/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowedByUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _unitOfWork.FollowRepository.GetFollowedByUser(id);
            return Ok(follows);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<FollowDto>> Follow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot follow yourself");
            var follow = await _unitOfWork.FollowRepository.GetFollow(user.Id, id);

            if (follow == null) follow = await _unitOfWork.FollowRepository.Follow(user.Id, id);
            else return BadRequest("User already followed");
            if (await _unitOfWork.SaveChangesAsync())
                return Ok(follow);
            else return BadRequest("Following failed");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unfollow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot unfollow yourself");
            await _unitOfWork.FollowRepository.Unfollow(user.Id, id);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
        private async Task<User> getUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}