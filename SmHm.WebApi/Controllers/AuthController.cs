using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
using SmHm.WebApi.Contracts.Auth;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IHttpContextAccessor _context;

        public AuthController(IAuthService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterUser([FromBody] RegisterRequest request)
        {
            var userId = await _service.Register(
                request.UserName,
                request.Email,
                request.Password
            );

            var response = new RegisterResponse(
                userId,
                request.Email,
                request.UserName
            );

            return Created(
                $"/api/users/{userId}",
                response
            );
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var (userId, token, userName) = await _service.Login(request.Email, request.Password);

            _context.HttpContext!.Response.Cookies.Append("jwt-service-staff", token);

            var response = new LoginResponse(
                userId,
                token,
                request.Email,
                userName
            );

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            _context.HttpContext!.Response.Cookies.Delete("jwt-service-staff");

            return NoContent();
        }
    }
}
