using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.WebApi.Contracts
{
    public record RoomResponse(
        Guid Id,
        string Name,
        string Description,
        RoomType RoomType,
        int Floor,
        List<Device> Devices);
}
