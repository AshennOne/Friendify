using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public User Author { get; set; }

        public string AuthorId { get; set; }
        public string TextContent { get; set; }
        public string ImgUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
        [JsonIgnore]
        public List<Comment> Comments { get; set; } = new List<Comment>();
         public int RepostedFromId{get;set;}
        public int RepostCount{get;set;} = 0;

    }
}