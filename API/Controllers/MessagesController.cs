using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public MessagesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("last")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetLastMessages()
        {
            var currentUser = await GetUserAsync();
            var messages = await _unitOfWork.MessageRepository.GetLastMessages(currentUser);
            return Ok(messages);
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string id)
        {
            var currentUser = await GetUserAsync();
            var messages = await _unitOfWork.MessageRepository.GetMessageThread(currentUser.Id, id);
            await _unitOfWork.SaveChangesAsync();
            return Ok(messages);
        }
        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] CreateMessageDto createMessageDto)
        {
            var currentUser = await GetUserAsync();
           var message = await _unitOfWork.MessageRepository.SendMessage(currentUser.Id, createMessageDto.UserId, createMessageDto.Content);
            if (await _unitOfWork.SaveChangesAsync()) return Ok(message);
            return BadRequest("Error while sending");
        }
        private async Task<User> GetUserAsync()
        {
            var username = User.GetUsernameFromToken();
            return await _userManager.FindByNameAsync(username);
        }
    }
}