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
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO authRequest)
        {
            bool isAuthenticated = await _authService.AuthenticateUserAsync(authRequest.Email, authRequest.Password);
            return isAuthenticated ? Ok("Login successful") : Unauthorized("Invalid credentials");
        }

    }
}
