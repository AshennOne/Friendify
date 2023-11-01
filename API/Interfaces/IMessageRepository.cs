using API.Entities;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> GetLastMessages(User user);
        public Task<IEnumerable<Message>> GetMessageThread(string currentUserId, string viewedUserId);
        public Task SendMessage(string senderId, string receiverId, string content);


    }
}