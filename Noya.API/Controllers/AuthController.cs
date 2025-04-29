using Microsoft.AspNetCore.Mvc;
using Noya.BLL.Services;
using Noya.Models;
using Noya.Models.Noya.Models;

namespace Noya.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            _authService = new AuthService();
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var user = _authService.Login(request.Email, request.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(user);
        }
    }
}
