namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for a follow relationship between users.
    /// </summary>
    public class FollowDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the follow relationship.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who is the follower.
        /// </summary>
        public string FollowerId { get; set; }
        /// <summary>
        ///  Gets or sets the identifier of the user who is being followed.
        /// </summary>
        public string FollowedId { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the follow relationship was established.
        /// </summary>
        public DateTime FollowDate { get; set; }
    }
}