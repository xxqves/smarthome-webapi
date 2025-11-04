using Microsoft.AspNetCore.Mvc;
using SmHm.Core.Abstractions;
using SmHm.Core.Models;
using SmHm.WebApi.Contracts;

namespace SmHm.WebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
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
                r.Devices));

            return Ok(response);
        }

        [HttpPost("rooms/add")]
        public async Task<ActionResult<Guid>> CreateRoom([FromBody] RoomRequest request)
        {
            var room = Room.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.RoomType,
                request.Floor,
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
