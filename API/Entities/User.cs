using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgUrl { get; set; } = null;
        public string Gender { get; set; }
        public DateTime DateOfBirth{get;set;} 
        [JsonIgnore]
        public List<Post> Posts{get;set;} = new List<Post>();
    }
}