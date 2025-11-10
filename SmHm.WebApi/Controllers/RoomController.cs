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
        private readonly ICurrentUserService _currUserService;

        public RoomController(IRoomService service, ICurrentUserService currUserService)
        {
            _service = service;
            _currUserService = currUserService;
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
            var userId = _currUserService.UserId;

            var room = Room.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.RoomType,
                request.Floor,
                userId,
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
