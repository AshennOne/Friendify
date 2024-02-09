using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for user login information.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the username or email entered by the user for login.
        /// </summary>

        [Required(ErrorMessage ="Username or Email is required")]
        public string UserNameOrEmail{get;set;}
        /// <summary>
        /// Gets or sets the password entered by the user for login.
        /// </summary>

        [Required(ErrorMessage ="Password is required")]
        public string Password{get;set;}
    }
}