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
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet("devices/get/all")]
        public async Task<ActionResult<List<DeviceResponse>>> GetDevices()
        {
            var devices = await _service.GetAllDevices();

            var response = devices.Select(d => new DeviceResponse(
                d.Id,
                d.Name,
                d.Description,
                d.DeviceType,
                d.IsEnabled,
                d.RoomId));

            return Ok(response);
        }

        [HttpPost("devices/add")]
        public async Task<ActionResult<Guid>> CreateDevice([FromBody] DeviceRequest request)
        {
            var device = Device.Create(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.DeviceType,
                request.RoomId);

            await _service.CreateDevice(device);

            return Ok(device.Id);
        }

        [HttpPut("devices/update/{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateDevice(Guid id, [FromBody] DeviceRequest request)
        {
            await _service.UpdateDevice(
                id,
                request.Name,
                request.Description,
                request.DeviceType,
                request.RoomId);

            return Ok(id);
        }

        [HttpDelete("devices/delete/{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteDevice(Guid id)
        {
            await _service.DeleteDevice(id);

            return Ok(id);
        }
    }
}
