using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
using SmHm.Core.Models;
using SmHm.WebApi.Contracts;

namespace SmHm.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;
        private readonly IHttpContextAccessor _context;

        public RoomController(IRoomService service, IHttpContextAccessor context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("rooms/get/all")]
        public async Task<ActionResult<List<RoomResponse>>> GetRooms()
        {
            var rooms = await _service.GetAllRooms();

            var response = rooms.Select(r => new RoomResponse(
                r.Id,
                r.Name,
                r.Description,
                r.RoomType,
                r.Floor,
                r.UserId,
                r.Devices));

            return Ok(response);
        }

        [HttpPost("rooms/add")]
        public async Task<ActionResult<Guid>> CreateRoom([FromBody] RoomRequest request)
        {
            var userIdClaim = _context.HttpContext?.User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            var room = Room.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.RoomType,
                request.Floor,
                Guid.Parse(userIdClaim),
                new List<Device>());

            await _service.CreateRoom(room);

            return Ok(room.Id);
        }

        [HttpPut("rooms/update/{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRoom(Guid id, [FromBody] RoomRequest request)
        {
            await _service.UpdateRoom(
                id,
                request.Name,
                request.Description,
                request.RoomType,
                request.Floor);

            return Ok(id);
        }

        [HttpDelete("rooms/delete/{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteRoom(Guid id)
        {
            await _service.DeleteRoom(id);

            return Ok(id);
        }
    }
}
