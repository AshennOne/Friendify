using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotificationsController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationsController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsForUser()
        {
            var username = User.GetUsernameFromToken();
            var user = await _userManager.FindByNameAsync(username);
            var notifications = _unitOfWork.NotificationRepository.GetNotifications(user);
            return Ok(notifications);
        }
    }
}