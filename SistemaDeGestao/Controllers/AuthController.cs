using Microsoft.AspNetCore.Mvc;
using SistemaDeGestao.Services;

namespace SistemaDeGestao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            bool isAuthenticated = await _authService.AuthenticateUserAsync(email, password);
            if (isAuthenticated)
                return Ok("Login successful");

            return Unauthorized("Invalid credentials");
        }
    }
}
