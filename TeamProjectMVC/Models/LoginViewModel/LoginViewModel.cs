using System.ComponentModel.DataAnnotations;

namespace TeamProjectMVC.Models.LoginViewModel
{
    public class LoginViewModel
    {
        
     
        public string Email { get; set; }

        
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must have at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 digit, and 1 special character.")]
        public string Password { get; set; }
    }
}
