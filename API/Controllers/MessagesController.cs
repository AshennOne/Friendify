using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manages message-related operations including creating, getting, updating and deleting messages.
    /// </summary>
    public class MessagesController : BaseApiController
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
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userManager"></param>
        public MessagesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;

        }
        /// <summary>
        /// Retrieves the last message from every conversation with the current user.
        /// </summary>
        /// <returns>Status code of operation with list of last messages in every conversation</returns>
        [HttpGet("last")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetLastMessages()
        {
            var currentUser = await GetUserAsync();
            var messages = await _unitOfWork.MessageRepository.GetLastMessages(currentUser);
            return Ok(messages);
        }
        /// <summary>
        /// Retrieves the message thread between the current user and the user with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code of operation with list of messages in thread with specified user</returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string id)
        {
            var currentUser = await GetUserAsync();
            var messages = await _unitOfWork.MessageRepository.GetMessageThread(currentUser.Id, id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(messages);
        }
        /// <summary>
        /// Sends a message from the current user to the user with the specified ID.
        /// </summary>
        /// <param name="createMessageDto"></param>
        /// <returns>Status code of operation with new message object</returns>
        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] CreateMessageDto createMessageDto)
        {
            var currentUser = await GetUserAsync();
           var message = await _unitOfWork.MessageRepository.SendMessage(currentUser.Id, createMessageDto.UserId, createMessageDto.Content);
            if (await _unitOfWork.SaveChangesAsync()) return Ok(message);
            return BadRequest("Error while sending");
        }
        /// <summary>
        /// Retrieves the current user asynchronously.
        /// </summary>
        /// <returns>Current user data</returns>
        private async Task<User> GetUserAsync()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}