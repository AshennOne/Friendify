using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages operations related to notifications, including creating notifications and marking them as read.
    /// </summary>
    public class NotificationsController : BaseApiController
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
        /// Initializes a new instance of the <see cref="NotificationsController"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public NotificationsController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        /// <summary>
        /// Retrieves notifications for the current user.
        /// </summary>
        /// <returns>Status code of operation with list of notifications for user</returns>
        /// <response code="200">If notifications has been retrieved sucessfuly</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsForUser()
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            var notifications = await _unitOfWork.NotificationRepository.GetNotifications(user);
            return Ok(notifications);
        }
        /// <summary>
        /// Marks a notification as read.
        /// </summary>
        /// <param name="id">id of notification that you want to read</param>
        /// <returns>Status code of operation with last read notification</returns>
        /// <response code="204">If notification has been read sucessfuly</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Notification>> ReadNotification(int id)
        {
            var notification = await _unitOfWork.NotificationRepository.GetNotificationById(id);
            notification.IsRead = true;
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}