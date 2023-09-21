namespace API.Entities
{
    public class Comment
    {   
        public int Id { get; set; }
        public string CommentedById { get; set; }
        public User CommentedBy{get;set;}
        public int PostId { get; set; }
        public Post Post{get;set;}
        public string Content {get;set;}
        public DateTime Created {get;set;} = DateTime.UtcNow;
    }
}