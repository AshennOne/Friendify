namespace API.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public UserClientDto Sender { get; set; }
        public string ReceiverId { get; set; }
        public UserClientDto Receiver { get; set; }
        public bool Read { get; set; } = false;
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
        public DateTime ReadDate { get; set; }
        public string Content { get; set; }
    }
}