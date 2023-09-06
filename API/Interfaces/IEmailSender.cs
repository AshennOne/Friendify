namespace API.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string body, string email);
    }
}