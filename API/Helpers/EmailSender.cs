using API.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace API.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public void SendEmail(string body, string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Friendlify Confirmation", "friendlify@noreply.com"));
            emailMessage.To.Add(new MailboxAddress("email", email));
            emailMessage.Subject = "Confirm your address email";
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