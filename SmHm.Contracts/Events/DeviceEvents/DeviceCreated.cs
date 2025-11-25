using SmHm.Core.Enums;

namespace SmHm.Contracts.Events.DeviceEvents
{
    public record DeviceCreated(
        Guid DeviceId,
        DeviceType DeviceType,
        Guid RoomId,
        Guid UserId,
        string UserName,
        DateTime CreatedAt);
}
