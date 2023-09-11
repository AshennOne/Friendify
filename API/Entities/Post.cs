namespace API.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int AuthorId{get;set;}
        public User Author{get;set;}
        public string TextContent{get;set;}
        public string ImgUrl{get;set;}
        public DateTime Created{get;set;}

    }
}