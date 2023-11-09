using System.ComponentModel.DataAnnotations;

namespace TeamProjectMVC.Models.RegisterViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
       
        public string Username  { get; set; }

        [Required(ErrorMessage = "Email  is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
    
        public string Password { get; set; }

       
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password  do not match")]
        public string ConfirmPassword { get; set; }
    }
}
