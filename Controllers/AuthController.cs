using Container_App.Model;
using Container_App.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Container_App.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User request)
        {
            var token = await _authService.Login(request.Username, request.Password);
            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] Token request)
        {
            var token = await _authService.RefreshToken(request.RefreshToken);
            if (token == null) return Unauthorized();

            return Ok(token);
        }
    }
}
