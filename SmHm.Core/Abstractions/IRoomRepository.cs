using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions
{
    public interface IRoomRepository
    {
        Task<Guid> Create(Room room);
        Task<Guid> Delete(Guid id);
        Task<List<Room>> Get();
        Task<Guid> Update(Guid id, string name, string desc, RoomType roomType, int floor);
    }
}