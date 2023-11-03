using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public MessageRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<MessageDto>> GetLastMessages(User user)
        {
            var userId = user.Id;

            var recentMessages = await _dbContext.Messages.Include(m => m.Receiver)
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g => g.OrderByDescending(m => m.SendDate).First())
                .ToListAsync();
            
            return _mapper.Map<IEnumerable<MessageDto>>(recentMessages);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string viewedUserId)
        {
            var messages = await _dbContext.Messages.Where(m => (m.SenderId == currentUserId && m.ReceiverId == viewedUserId) || (m.SenderId == viewedUserId && m.ReceiverId == currentUserId)).ToListAsync();
            messages.ForEach(element =>
            {
                if (element.ReceiverId == currentUserId) element.Read = true;
            });

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };
            await _dbContext.AddAsync(message);
        }
    }
}