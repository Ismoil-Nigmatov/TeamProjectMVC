using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Entity.Enums;
using TeamProjectMVC.Models.LoginViewModel;
using TeamProjectMVC.Models.RegisterViewModel;

namespace TeamProjectMVC.Service
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager) => _userManager = userManager;

        public string Validate(ModelStateDictionary modelState, LoginViewModel loginViewModel)
        {
            if (!modelState.IsValid)
            {
                var emailError = modelState["Email"]?.Errors.FirstOrDefault();
                if (emailError != null)
                {
                    return emailError.ErrorMessage;
                }

            }
            else
            {
                if (!(IsValidEmail(loginViewModel.Email)))
                {
                    return "Please enter valid email address!";
                }
            }

            var validatePassword = ValidatePassword(loginViewModel.Password);
            if (!validatePassword)
            {
                return "The password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
            }

            if (!CheckUser(loginViewModel).Result)
            {
                return "Email or Password incorrect";
            }
            return "success";
        }
        public bool IsValidEmail(string emailAddress)
        {
            string emailPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(emailAddress);
        }
        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Check if the password is at least 8 characters long.
            if (password.Length < 8)
            {
                return false;
            }

            // Check if the password contains at least one uppercase letter.
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Check if the password contains at least one lowercase letter.
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            // Check if the password contains at least one digit.
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Check if the password contains at least one special character.
            if (!password.Any(char.IsPunctuation))
            {
                return false;
            }

            // The password is valid.
            return true;
        }
        public async Task<bool> CheckUser(LoginViewModel loginView)
        {
            var user = await _userManager.FindByEmailAsync(loginView.Email);
            if (user == null)
            {
                return false;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginView.Password);
            if (!passwordCheck)
            {
                return false;
            }

            return true;
        }

        public async Task<string> ValidateRegistration(ModelStateDictionary modelState, RegisterViewModel model)
        {
            if (model.Username.Length < 6)
            {
                return "Username must be at least 6 characters long";
            }
            
            if (!IsValidEmail(model.Email))
            {
                return "Email is invalid";
            }

            if (!ValidatePassword(model.Password))
            {
                return "The password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
            }

            if (model.Password != model.ConfirmPassword)
            {
                return "Confirm password does not match password";
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return "This email address is already in use";
            }

            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, (ERole.USER).ToString());
            return "success";
        }
    }
}
