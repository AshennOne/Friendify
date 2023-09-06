using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="First Name is required")]
        [MinLength(2,ErrorMessage ="Your First Name is too short")]
        [MaxLength(18,ErrorMessage ="Your First Name is too long")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is required")]
        [MinLength(2,ErrorMessage ="Your Last Name is too short")]
        [MaxLength(18,ErrorMessage ="Your Last Name is too long")]
        public string LastName {get;set;}
        [Required(ErrorMessage ="Username is required")]
        [MinLength(4,ErrorMessage ="Your Username is too short")]
        [MaxLength(18,ErrorMessage ="Your Username is too long")]
        public string UserName{get;set;}
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email is not valid")]
        public string Email{get;set;}
        [Required(ErrorMessage ="Password is required")]
        public string Password{get;set;}
    }
}