using System.Text.Json.Serialization;

namespace API.Entities
{
    /// <summary>
    /// This class represents a follow relationship entity in the application.
    /// </summary>
    public class Follow
    {
        /// <summary>
        /// Gets or sets the unique identifier of the follow relationship.
        /// </summary>
        public int Id{get;set;}
        /// <summary>
        /// Gets or sets the user who is following.
        /// </summary>

        public User Follower{get;set; }
        /// <summary>
        /// Gets or sets the identifier of the user who is following.
        /// </summary>
        public string FollowerId { get; set; }
        /// <summary>
        /// Gets or sets the user who is being followed.
        /// </summary>
        public User Followed { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who is being followed.
        /// </summary>
        public string FollowedId { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the follow relationship was created. Defaults to the current UTC time.
        /// </summary>
        public DateTime FollowDate{get;set;} = DateTime.UtcNow;
    }
}