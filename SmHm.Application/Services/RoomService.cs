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

        public async Task<List<Room>> GetAllRooms()
        {
            return await _repository.Get();
        }

        public async Task<Guid> CreateRoom(Room room)
        {
            return await _repository.Create(room);
        }

        public async Task<Guid> UpdateRoom(Guid id, string name, string desc, RoomType roomType, int floor)
        {
            return await _repository.Update(id, name, desc, roomType, floor);
        }

        public async Task<Guid> DeleteRoom(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
