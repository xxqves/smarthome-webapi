using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions
{
    public interface IRoomRepository
    {
        Task<Guid> Create(Room room, CancellationToken cancellationToken = default);
        Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default);
        Task<List<Room>> Get(CancellationToken cancellationToken = default);
        Task<Guid> Update(Guid id, string name, string desc, RoomType roomType, int floor, CancellationToken cancellationToken = default);
    }
}