using System;
using Domain.Authentication;
using Domain.Countries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _jwtService;

        public UserController(AuthService authenticationService)
        {
            _jwtService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            var jwt = _jwtService.Generate("1ccc377c-6090-42d7-a74f-8f3cd49357a2");

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new { message = "success" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "success" });
        }
    }
}
