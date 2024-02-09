namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for editing user information.
    /// </summary>
    public class EditUserDto
    {
        /// <summary>
        /// Gets or sets the URL of the user's profile image.
        /// </summary>
        public string ImgUrl{get;set;}
        /// <summary>
        /// Gets or sets the biography or description of the user.
        /// </summary>
        public string Bio{get;set;}
    }
}