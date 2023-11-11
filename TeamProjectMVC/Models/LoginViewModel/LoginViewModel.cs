using System.ComponentModel.DataAnnotations;
using TeamProjectMVC.ValidationAttributes;

namespace TeamProjectMVC.Models.LoginViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required.")]
        [CustomEmail(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Email or password is incorrect.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Email or password is incorrect.")]
        public string Password { get; set; }
    }
}
