namespace API.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public string LikedById { get; set; }
        public User LikedBy{get;set;}
        public int PostId { get; set; }
        public Post Post{get;set;}

    }
}