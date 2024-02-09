using System.ComponentModel.DataAnnotations;
using API.CustomValidators;
namespace API.Dtos
{
    /// <summary>
    /// This class represents a data transfer object (DTO) for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [NameCharacter(ErrorMessage ="First name cannot include any special characters")]
        [Required(ErrorMessage ="First Name is required")]
        [MinLength(2,ErrorMessage ="Your First Name is too short")]
        [MaxLength(18,ErrorMessage ="Your First Name is too long")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [NameCharacter(ErrorMessage = "Last name cannot include any special characters")]
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Your Last Name is too short")]
        [MaxLength(18, ErrorMessage = "Your Last Name is too long")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the username chosen by the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4, ErrorMessage = "Your Username is too short")]
        [MaxLength(18, ErrorMessage = "Your Username is too long")]
        [UsernameCharacter(ErrorMessage = "Username cannot include any special characters except '-_.'")]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password chosen by the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Your Password is too short")]
        [PasswordContainsUppercaseAndDigit(ErrorMessage = "Password must have at least one uppercase letter and one digit")]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        [Gender]
        public string Gender { get; set; }
        /// <summary>
        ///  Gets or sets the confirmation of the password chosen by the user.
        /// </summary>
        [Required(ErrorMessage = "Password confirmation is required")]
        [ComparePasswords("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        [Required]
        [MinimumAge(18,ErrorMessage="You must be at least 18 years old")]
        public DateTime DateOfBirth{get;set;}
    }
}