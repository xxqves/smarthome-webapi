using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
using SmHm.WebApi.Contracts.Users;

namespace SmHm.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IHttpContextAccessor _context;

        public UserController(IUserService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost("users/register")]
        public async Task<ActionResult<Guid>> RegisterUser([FromBody] RegisterUserRequest request)
        {
            return await _service.Register(request.UserName, request.Email, request.Password);
        }

        [HttpPost("users/login")]
        public async Task<ActionResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _service.Login(request.Email, request.Password);

            _context.HttpContext!.Response.Cookies.Append("jwt-service-staff", token);

            return Ok();
        }
    }
}
