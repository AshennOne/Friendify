namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for authorized users.
    /// </summary>
    public class AuthorizedUserDto
    {
        /// <summary>
        /// Gets or sets the username of the authorized user.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email of the authorized user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the token of the authorized user.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the image url of the authorized user.
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// Gets or sets a boolean value indicating whether the email of the authorized user is confirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; }
    }
}