using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [JsonIgnore]
        public string ToUserId{get;set;}
        public bool IsRead{get;set;}
        public DateTime CreateDate{get;set;} = DateTime.UtcNow;
        public string FromUserName { get; set; }
        public string Message { get; set; }
        public NotiType Type{get;set;}
        public int ElementId{get;set;}
        public string ImgUrl { get; set; }

    }
}