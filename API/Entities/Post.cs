using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User Author{get;set;}
        public string AuthorId { get; set; }
        public string TextContent{get;set;}
        public string ImgUrl{get;set;}
        public DateTime Created{get;set;}= DateTime.UtcNow;

    }
}