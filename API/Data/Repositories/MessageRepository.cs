using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    /// <summary>
    /// This class implements the IMessageRepository interface and provides methods to interact with messages in the database.
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        /// <summary>
        /// Represents the application's database context used for interacting with the underlying database.
        /// </summary>
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        /// Represents an instance of IMapper used for object mapping between different types within the application.
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class with the specified ApplicationDbContext and IMapper.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public MessageRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }
        /// <summary>
        /// Retrieves the last messages exchanged by a user, grouped by conversation partner.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns a collection of MessageDto objects representing the last messages exchanged by the user.</returns>
        public async Task<IEnumerable<MessageDto>> GetLastMessages(User user)
        {
            var userId = user.Id;

            var recentMessages = _dbContext.Messages.Include(m => m.Receiver).Include(m => m.Sender)
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g => g.OrderByDescending(m => m.SendDate).First()).AsEnumerable();
            IEnumerable<Message> messages = recentMessages.OrderByDescending(m => m.SendDate).ToList();
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
        /// <summary>
        /// Retrieves the message thread between two users.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="viewedUserId"></param>
        /// <returns>Returns a collection of MessageDto objects representing the message thread between the two users.</returns>
        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string viewedUserId)
        {
            var messages = await _dbContext.Messages.Include(m => m.Receiver).Include(m => m.Sender).Where(m => (m.SenderId == currentUserId && m.ReceiverId == viewedUserId) || (m.SenderId == viewedUserId && m.ReceiverId == currentUserId)).ToListAsync();
            messages.ForEach(element =>
            {
                if (element.ReceiverId == currentUserId) element.Read = true;
            });

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
        /// <summary>
        /// Sends a message from one user to another.
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <param name="content"></param>
        /// <returns>Returns a Task representing the asynchronous operation, which resolves to a MessageDto object representing the sent message.</returns>
        public async Task<MessageDto> SendMessage(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };
            await _dbContext.AddAsync(message);
            return _mapper.Map<MessageDto>(message);
        }
    }
}