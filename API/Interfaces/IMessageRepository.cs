using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Interface for managing messages between users.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Retrieves the last messages of a user.
        /// </summary>
        /// <param name="user">The user whose last messages are to be retrieved.</param>
        /// <returns>An asynchronous task returning an enumerable collection of last messages.</returns>
        public IEnumerable<MessageDto> GetLastMessages(User user);
        /// <summary>
        /// Retrieves the message thread between two users.
        /// </summary>
        /// <param name="currentUserId">The ID of the current user.</param>
        /// <param name="viewedUserId">The ID of the user whose messages are being viewed.</param>
        /// <returns>An asynchronous task returning an enumerable collection of messages in the thread.</returns>
        public Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string viewedUserId);
        /// <summary>
        /// Sends a message from one user to another.
        /// </summary>
        /// <param name="senderId">The ID of the sender.</param>
        /// <param name="receiverId">The ID of the receiver.</param>
        /// <param name="content">The content of the message.</param>
        /// <returns>An asynchronous task returning the sent message.</returns>
        public Task<MessageDto> SendMessage(string senderId, string receiverId, string content);


    }
}