using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Data;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Entity.Enums;
using TeamProjectMVC.Models.LoginViewModel;
using TeamProjectMVC.Models.RegisterViewModel;

namespace TeamProjectMVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _appDbContext;


        public AccountController(UserManager<User> userManager, AppDbContext appDbContext, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            // Validate email
            if (!ModelState.IsValid)
            {
                var emailError = ModelState["Email"].Errors.FirstOrDefault();
                if (emailError != null)
                {
                    TempData["Error"] = emailError.ErrorMessage;
                    return View(loginViewModel);
                }
            }

            // Validate password
            if (!ModelState.IsValid)
            {
                var passwordError = ModelState["Password"].Errors.FirstOrDefault();
                if (passwordError != null)
                {
                    TempData["Error"] = passwordError.ErrorMessage;
                    return View(loginViewModel);
                }
            }

            // Check if user exists
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return View(loginViewModel);
            }

            // Check if password is correct
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!passwordCheck)
            {
                TempData["Error"] = "Password is incorrect.";
                return View(loginViewModel);
            }

            // Login is successful
            await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get all ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();

                // Add all ModelState errors to TempData
                foreach (var error in errors)
                {
                    TempData["Error"] = error.ErrorMessage;
                }

                // Return to the register view
                return View(model);
            }

            // Check if the email address is already in use
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            // Create a new user
            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if (!newUserResponse.Succeeded)
            {
                TempData["Error"] = "An error occurred while creating the new user.";
                return View(model);
            }

            // Add the new user to the User role
            await _userManager.AddToRoleAsync(newUser, (ERole.USER).ToString());

            // Redirect the user to the home page
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
