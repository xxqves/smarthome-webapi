using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
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
        public async Task<ActionResult<Guid>> RegisterUser([FromBody] RegisterRequest request)
        {
            return await _service.Register(request.UserName, request.Email, request.Password);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _service.Login(request.Email, request.Password);

            _context.HttpContext!.Response.Cookies.Append("jwt-service-staff", token);

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            _context.HttpContext!.Response.Cookies.Delete("jwt-service-staff");

            return NoContent();
        }
    }
}
