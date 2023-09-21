namespace API.Dtos
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public UserClientDto CommentedBy{get;set;}
        public PostDto Post{get;set;}
        public DateTime Created{get;set;}
        public string Content {get;set;}
    }
}