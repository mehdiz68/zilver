using Microsoft.AspNetCore.Mvc;
using zilver.auth.Interfaces;
using zilver.auth.Models;

namespace zilver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // --------------------------
        // Login → returns JWT + RefreshToken
        // --------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        // --------------------------
        // Refresh Token → returns new JWT + refreshToken
        // --------------------------
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto model)
        {
            var result = await _authService.RefreshTokenAsync(model.RefreshToken);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; }
    }
}
