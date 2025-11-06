using Microsoft.EntityFrameworkCore;
using SmHm.Core.Abstractions;
using SmHm.Core.Enums;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly SmartHomeDbContext _context;

        public RoomRepository(SmartHomeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> Get(CancellationToken cancellationToken = default)
        {
            var roomEntities = await _context.Rooms
                .AsNoTracking()
                .Include(r => r.Devices)
                .ToListAsync(cancellationToken);

            var rooms = roomEntities
                .Select(r => Room.Create(
                    r.Id,
                    r.Name,
                    r.Description,
                    r.RoomType,
                    r.Floor,
                    r.UserId,
                    r.Devices
                    .Select(d => Device.Create(
                        d.Id,
                        d.Name,
                        d.Description,
                        d.DeviceType,
                        d.RoomId))
                    .ToList()))
                .ToList();

            return rooms;
        }

        public async Task<Guid> Create(Room room, CancellationToken cancellationToken = default)
        {
            var roomEntity = new RoomEntity
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                RoomType = room.RoomType,
                Floor = room.Floor,
                UserId = room.UserId,
                Devices = new List<DeviceEntity>()
            };

            await _context.Rooms.AddAsync(roomEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return roomEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string desc, RoomType roomType, int floor, CancellationToken cancellationToken = default)
        {
            await _context.Rooms
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, r => name)
                    .SetProperty(r => r.Description, r => desc)
                    .SetProperty(r => r.RoomType, r => roomType)
                    .SetProperty(r => r.Floor, r => floor), cancellationToken);

            return id;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Rooms
                .Where(r => r.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return id;
        }
    }
}
