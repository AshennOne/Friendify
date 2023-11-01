using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MessageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<Message>> GetLastMessages(User user)
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<Message>> GetMessageThread(string currentUserId, string viewedUserId)
        {
            var messages =await _dbContext.Messages.Where(m => (m.SenderId == currentUserId && m.ReceiverId == viewedUserId)||(m.SenderId == viewedUserId && m.ReceiverId == currentUserId) ).ToListAsync();
            messages.ForEach(element => {
                if(element.ReceiverId == currentUserId) element.Read = true;
            });
            return messages;
        }

        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var message = new Message{
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };
            await _dbContext.AddAsync(message);
        }
    }
}