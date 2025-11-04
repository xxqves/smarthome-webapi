using SmHm.Core.Enums;

namespace SmHm.WebApi.Contracts
{
    public record DeviceResponse(
        Guid id,
        string Name,
        string Description,
        DeviceType DeviceType,
        bool IsEnabled,
        Guid RoomId);
}
