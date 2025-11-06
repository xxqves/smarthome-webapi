using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
using SmHm.WebApi.Contracts;

namespace SmHm.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("users/register")]
        public async Task<ActionResult<Guid>> RegisterUser(RegisterUserRequest request)
        {
            return await _service.Register(request.UserName, request.Email, request.Password);
        }

        [HttpPost("users/login")]
        public async Task<ActionResult<Guid>> Login(LoginUserRequest request)
        {
            var token = await _service.Login(request.Email, request.Password);

            return Ok(token);
        }
    }
}
