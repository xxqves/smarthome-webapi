using SmHm.Core.Enums;

namespace SmHm.WebApi.Contracts.Rooms
{
    public record RoomRequest(
        string Name,
        string Description,
        RoomType RoomType,
        int Floor);
}
