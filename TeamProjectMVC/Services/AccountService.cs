using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Entity.Enums;
using TeamProjectMVC.Models.LoginViewModel;
using TeamProjectMVC.Models.RegisterViewModel;

namespace TeamProjectMVC.Services
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager) => _userManager = userManager;

        public async Task<string> HandleModelStateErrors(ModelStateDictionary modelState, string defaultErrorMessage = "Validation failed.")
        {
            if (modelState.IsValid) return "success";

            var errorMessages = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return errorMessages.Count > 0 ? errorMessages[0] : defaultErrorMessage;
        }

        public async Task<(bool IsAuthenticated, string UserRole, string UserId)> CheckUserAsync(LoginViewModel loginView)
        {
            var user = await _userManager.FindByEmailAsync(loginView.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginView.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                return (true, roles.FirstOrDefault(), user.Id)!;
            }

            return (false, null, null)!;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, ERole.USER.ToString());
            return true;
        } 
    }
}
