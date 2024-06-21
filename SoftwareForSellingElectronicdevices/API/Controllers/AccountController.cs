using Data.DTOs;
using Data.IRespositories;
using Data.Models;
using Data.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAccount account, SignInManager<User> signInManager)
        {
            _account = account;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegister)
        {
            return Ok(await _account.RegisterAsync(userRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            return Ok(await _account.LogInAsync(userLogin));
        }
    }
}
