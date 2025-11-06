using SmHm.Core.Abstractions;
using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Room>> GetAllRooms(CancellationToken cancellationToken = default)
        {
            return await _repository.Get(cancellationToken);
        }

        public async Task<Guid> CreateRoom(Room room, CancellationToken cancellationToken = default)
        {
            return await _repository.Create(room, cancellationToken);
        }

        public async Task<Guid> UpdateRoom(Guid id, string name, string desc, RoomType roomType, int floor, CancellationToken cancellationToken = default)
        {
            return await _repository.Update(id, name, desc, roomType, floor, cancellationToken);
        }

        public async Task<Guid> DeleteRoom(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.Delete(id, cancellationToken);
        }
    }
}
