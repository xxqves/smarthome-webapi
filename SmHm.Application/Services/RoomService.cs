using SmHm.Contracts.Events.RoomEvents;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Messaging;
using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageBus _messageBus;

        public RoomService(IRoomRepository repository, ICurrentUserService currentUserService, IMessageBus messageBus)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _messageBus = messageBus;
        }

        public async Task<List<Room>> GetAllRooms(CancellationToken cancellationToken = default)
        {
            return await _repository.Get(cancellationToken);
        }

        public async Task<Guid> CreateRoom(Room room, CancellationToken cancellationToken = default)
        {
            var @event = new RoomCreated(
                room.Id,
                room.RoomType,
                _currentUserService.UserId,
                _currentUserService.UserName,
                DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, cancellationToken);

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
