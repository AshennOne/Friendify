using API.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Helpers
{
    /// <summary>
    /// Helper class for sending emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Represents the configuration settings required for sending emails.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        /// <summary>
        /// Sends an email with the provided body, email address, and subject.
        /// </summary>
        /// <param name="body">The content of the email.</param>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        public void SendEmail(string body, string email,string subject)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Friendlify Confirmation", "friendlify@noreply.com"));
            emailMessage.To.Add(new MailboxAddress("email", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };
            using var client = new SmtpClient();
            try
            {
                client.Connect(_configuration["MailSettings:Server"], Convert.ToInt32(_configuration["MailSettings:Port"]), true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_configuration["MailSettings:UserName"], _configuration["MailSettings:Password"]);
                client.Send(emailMessage);

            }
            catch
            {
                throw;
            }
            finally
            {

                client.Disconnect(true);
                client.Dispose();
            }

        }
    }
}