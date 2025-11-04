using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions
{
    public interface IRoomService
    {
        Task<Guid> CreateRoom(Room room);
        Task<Guid> DeleteRoom(Guid id);
        Task<List<Room>> GetAllRooms();
        Task<Guid> UpdateRoom(Guid id, string name, string desc, RoomType roomType, int floor);
    }
}