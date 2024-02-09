using API.Entities;

namespace API.Dtos
{
    /// <summary>
    /// This class represents a simplified version of user data for client-side consumption.
    /// </summary>
    public class UserClientDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the URL of the user's profile image.
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// Gets or sets the biography or description of the user.
        /// </summary>
        public string Bio { get; set; }
        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Gets or sets the list of users who follow this user.
        /// </summary>
        public List<FollowDto> Followers { get; set; }
        /// <summary>
        /// Gets or sets the list of users whom this user follows.
        /// </summary>
        public List<FollowDto> Followed { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string Id { get; set; }
    }
}