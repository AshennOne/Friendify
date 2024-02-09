using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    /// <summary>
    ///  Represents a user in the application.
    /// </summary>
    public class User:IdentityUser
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The URL of the user's profile image.
        /// </summary>
        public string ImgUrl { get; set; } = null;
        /// <summary>
        /// The gender of the user.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// The biography or description of the user.
        /// </summary>
        public string Bio { get; set; }
        /// <summary>
        /// The date of birth of the user.
        /// </summary>
        public DateTime DateOfBirth{get;set; }
        /// <summary>
        /// The list of posts created by the user.
        /// </summary>
        [JsonIgnore]
        public List<Post> Posts { get; set; } = new List<Post>();
        /// <summary>
        ///  The list of post likes associated with the user.
        /// </summary>
        [JsonIgnore]
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
        /// <summary>
        /// The list of comments made by the user.
        /// </summary>
        [JsonIgnore]
        public List<Comment> Comments{get;set;}= new List<Comment>();
        /// <summary>
        /// The list of followers of the user.
        /// </summary>
        public List<Follow> Followers{get;set;} = new List<Follow>();
        /// <summary>
        /// The list of users followed by the user.
        /// </summary>
        public List<Follow> Followed { get; set; } = new List<Follow>();
        /// <summary>
        /// The list of notifications received by the user.
        /// </summary>
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        /// <summary>
        /// The list of messages sent by the user.
        /// </summary>
        public List<Message> MessagesSend { get; set; } = new List<Message>();
        /// <summary>
        /// The list of messages received by the user.
        /// </summary>
        public List<Message> MessagesReceived{get;set;} = new List<Message>();
    }
}