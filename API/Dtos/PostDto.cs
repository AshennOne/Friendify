using API.Entities;

namespace API.Dtos
{
    public class PostDto
    {
        public int Id{get;set;}
        public string TextContent{get;set;}
        public string ImgUrl { get; set; }
        public DateTime Created { get; set; }
        public UserClientDto Author{get;set;}
        public UserClientDto OriginalAuthor{get;set;}
        public string OriginalAuthorId{get;set;}
        public int LikesCount{get;set;}
        public int CommentsCount{get;set;}   
        public int RepostCount{get;set;}
        public int RepostedFromId{get;set;}
    }
}