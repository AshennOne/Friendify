using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth{get;set;} 
        public List<Post> Posts{get;set;} = new List<Post>();
    }
}