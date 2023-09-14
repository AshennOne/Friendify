namespace API.Dtos
{
    public class PostDto
    {
        public int Id{get;set;}
        public string TextContent{get;set;}
        public string ImgUrl { get; set; }
        public DateTime Created { get; set; }
        public UserClientDto Author{get;set;}
    }
}