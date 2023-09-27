using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Follow
    {
        public int Id{get;set;}
        
        public User Follower{get;set;}
        public string FollowerId{get;set;}
       
        public User Followed{get;set;}
        public string FollowedId{get;set;}
        public DateTime FollowDate{get;set;} = DateTime.UtcNow;
    }
}