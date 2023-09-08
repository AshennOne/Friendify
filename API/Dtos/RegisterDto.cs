using System.ComponentModel.DataAnnotations;
using API.CustomValidators;
namespace API.Dtos
{
    public class RegisterDto
    {
        [NameCharacter(ErrorMessage ="First name cannot include any special characters")]
        [Required(ErrorMessage ="First Name is required")]
        [MinLength(2,ErrorMessage ="Your First Name is too short")]
        [MaxLength(18,ErrorMessage ="Your First Name is too long")]
        public string FirstName { get; set; }
        [NameCharacter(ErrorMessage ="Last name cannot include any special characters")]
        [Required(ErrorMessage ="Last Name is required")]
        [MinLength(2,ErrorMessage ="Your Last Name is too short")]
        [MaxLength(18,ErrorMessage ="Your Last Name is too long")]
        public string LastName {get;set;}
        [Required(ErrorMessage ="Username is required")]
        [MinLength(4,ErrorMessage ="Your Username is too short")]
        [MaxLength(18,ErrorMessage ="Your Username is too long")]
        [UsernameCharacter(ErrorMessage ="Username cannot include any special characters except '-_.'")]
        public string UserName{get;set;}
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email is not valid")]
        public string Email{get;set;}
        [Required(ErrorMessage ="Password is required")]
        [MinLength(4,ErrorMessage ="Your Password is too short")]
       [PasswordContainsUppercaseAndDigit(ErrorMessage ="Password must have at least one uppercase letter and one digit")]
        public string Password{get;set;}
         [Gender]
         public string Gender { get; set; }
          [Required(ErrorMessage ="Password confirmation is required")]
          [ComparePasswords("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MinimumAge(18,ErrorMessage="You must be at least 18 years old")]
        public DateTime DateOfBirth{get;set;}
    }
}