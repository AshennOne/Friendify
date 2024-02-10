using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Manages operations related to following users, including getting followers, followed users, following and unfollowing users.
    /// </summary>
    public class FollowController : BaseApiController
    {

        /// <summary>
        /// Provides access to the unit of work for interacting with the database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Manages user-related operations such as finding correct user.
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FollowController"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        public FollowController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        /// <summary>
        /// Retrieves followers for a specified user ID.
        /// </summary>
        /// <param name="id">id of user that you want to get list of followers for</param>
        /// <returns>Status code of operation with list of followers</returns>
        /// <response code="200">If follows has been retrieved successfuly</response>
        /// <response code="404">If current user doesn't exists</response>
        [HttpGet("followers/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowersForUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _unitOfWork.FollowRepository.GetFollowersForUser(id);
            return Ok(follows);
        }
        /// <summary>
        /// Retrieves users followed by a specified user ID.
        /// </summary>
        /// <param name="id">id of user that you want to get list of followed users for</param>
        /// <returns>Status code of operation with list of followed users</returns>
        /// <response code="200">If follows has been retrieved successfuly</response>
        /// <response code="404">If current user doesn't exists</response>
        [HttpGet("followed/{id}")]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetFollowedByUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            var follows = _unitOfWork.FollowRepository.GetFollowedByUser(id);
            return Ok(follows);
        }
        /// <summary>
        /// Follows a user.
        /// </summary>
        /// <param name="id">id of user that you want to follow</param>
        /// <returns>Status code of operation with new follow object</returns>
        /// <response code="200">If user had been followed sucessfuly</response>
        /// <response code="400">If user cannot be followed</response>
        [HttpPost("{id}")]
        public async Task<ActionResult<FollowDto>> Follow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot follow yourself");
            var follow = await _unitOfWork.FollowRepository.GetFollow(user.Id, id);
            var followed = await _userManager.FindByIdAsync(id);
            if (follow == null) await _unitOfWork.FollowRepository.Follow(user.Id, id);
            else return BadRequest("User already followed");
            if (await _unitOfWork.SaveChangesAsync())
            {
                follow = await _unitOfWork.FollowRepository.GetFollow(user.Id, id);
                await _unitOfWork.NotificationRepository.AddNotification(user, followed, NotiType.Follow, follow.Id);
                return Ok(follow);
            }

            else return BadRequest("Following failed");
        }
        /// <summary>
        /// Unfollows a user.
        /// </summary>
        /// <param name="id">id of user that you want to unfollow</param>
        /// <returns>Status code of operation</returns>
        /// <response code="204">If user had been unfollowed sucessfuly / was unfollowed before operation</response>
        /// <response code="400">If user try to unfollow yourself</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Unfollow(string id)
        {
            var user = await getUser();
            if (user.Id == id) return BadRequest("You cannot unfollow yourself");
            var follow = await _unitOfWork.FollowRepository.Unfollow(user.Id, id);
            if (await _unitOfWork.SaveChangesAsync())
            {
                await _unitOfWork.NotificationRepository.RemoveNotification(NotiType.Follow, follow.Id);
            }

            return NoContent();
        }
        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>Current user data</returns>
        private async Task<User> getUser()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}