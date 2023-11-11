﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Models.LoginViewModel;
using TeamProjectMVC.Models.RegisterViewModel;
using TeamProjectMVC.Services;

namespace TeamProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly SignInManager<User> _signInManager;


        public AccountController(SignInManager<User> signInManager, AccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            TempData.Clear();
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var validationError = await _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                TempData.Keep("Error");
                return View();
            }

            var (isAuthenticated, userRole, userId) = await _accountService.CheckUserAsync(loginViewModel);

            if (!isAuthenticated)
            {
                TempData["Error"] = "Email or v password is incorrect";
                return View();
            }

            return RedirectToAction("Product", "Product" , new {role = userRole , id = userId});
        }

        [HttpGet]
        public IActionResult Register()
        {
            TempData.Clear();
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var validationError = await _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                return View();
            }

            await _accountService.RegisterUser(model);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
