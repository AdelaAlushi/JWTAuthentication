using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {

        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public LoginController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authorize([FromBody] User user)
        {
            var token = jwtAuthenticationManager.Authenticate(user.UserName, user.Password);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }

        [HttpGet]
        public IActionResult TestRoute()
        {
            return Ok("Authorized");
        }
    }
}
