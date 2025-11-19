using SmHm.Core.Enums;

namespace SmHm.WebApi.Contracts.Devices
{
    public record DeviceRequest(
        string Name,
        string Description,
        DeviceType DeviceType,
        Guid RoomId);
}
