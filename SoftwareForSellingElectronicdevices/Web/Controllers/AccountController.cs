using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web.IServices;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            if (ModelState.IsValid)
            {
                await _accountService.RegisterAsync(userRegisterDTO);
                return RedirectToAction("Index", "Home");
            }
            return View(userRegisterDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LogInAsync(userLogin);
                if (result.success)
                {
                    await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, false, lockoutOnFailure: false);
                }
                return Redirect(userLogin?.ReturnUrl ?? "/home");
            }
            return View(userLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("login", "account");
        }

    }
}
