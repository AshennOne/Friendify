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
            var followed  = await _userManager.FindByIdAsync(id);
            if (follow == null)  await _unitOfWork.FollowRepository.Follow(user.Id, id);
            else return BadRequest("User already followed");
            if (await _unitOfWork.SaveChangesAsync())
            {
                follow =await _unitOfWork.FollowRepository.GetFollow(user.Id,id);
                await _unitOfWork.NotificationRepository.AddNotification(user, followed, NotiType.Follow, follow.Id);
                return Ok(follow);
            }

            else return BadRequest("Following failed");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unfollow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot unfollow yourself");
          var follow =  await _unitOfWork.FollowRepository.Unfollow(user.Id, id);
            if(await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.NotificationRepository.RemoveNotification(NotiType.Follow, follow.Id);
            }

            return NoContent();
        }
        private async Task<User> getUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}