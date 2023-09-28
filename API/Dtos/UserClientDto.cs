using API.Entities;

namespace API.Dtos
{
    public class UserClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
        public string Bio { get; set; }
        public List<FollowDto> Followers{get;set;}
        public List<FollowDto> Followed{get;set;}
        public string Id { get; set; }
    }
}