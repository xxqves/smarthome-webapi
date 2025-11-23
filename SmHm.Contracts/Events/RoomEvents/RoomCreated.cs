using SmHm.Core.Enums;

namespace SmHm.Contracts.Events.RoomEvents
{
    public record RoomCreated(
        Guid RoomId,
        RoomType RoomType,
        Guid UserId,
        string UserName,
        DateTime CreatedAt);
}
