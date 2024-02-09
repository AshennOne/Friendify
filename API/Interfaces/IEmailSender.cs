namespace API.Interfaces
{
    /// <summary>
    /// Interface for sending emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="body">The body content of the email.</param>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        void SendEmail(string body, string email,string subject);
    }
}