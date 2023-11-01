namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public User Sender{get;set;}
        public string ReceiverId { get; set; }
        public User Receiver{get;set;}
        public bool Read{get;set;} = false;
        public DateTime SendDate{get;set;} = DateTime.UtcNow;
        public DateTime ReadDate{get;set;}
        public string Content{get;set;}
    }
}