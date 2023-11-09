using System.ComponentModel.DataAnnotations;

namespace TeamProject.Entity.LoginViewModel
{
    public class LoginViewModel
    {


        [Required(ErrorMessage ="Email address is required")]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }

    }
}
