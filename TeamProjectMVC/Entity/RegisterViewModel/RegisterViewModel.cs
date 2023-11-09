using System.ComponentModel.DataAnnotations;

namespace TeamProject.Entity.RegisterViewModel
{
    public class RegisterViewModel
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Usernameis required")]
        [StringLength(12, MinimumLength = 6)]
        public string Username { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }


        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }



    }
}
