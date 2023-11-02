using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<MessageDto>> GetLastMessages(User user);
        public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string viewedUserId);
        public Task SendMessage(string senderId, string receiverId, string content);


    }
}