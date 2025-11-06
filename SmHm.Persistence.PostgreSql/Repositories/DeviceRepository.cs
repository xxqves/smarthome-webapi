using Microsoft.EntityFrameworkCore;
using SmHm.Core.Abstractions;
using SmHm.Core.Enums;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly SmartHomeDbContext _context;

        public DeviceRepository(SmartHomeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> Get(CancellationToken cancellationToken = default)
        {
            var deviceEntities = await _context.Devices
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var devices = deviceEntities
                .Select(d => Device.Create(
                    d.Id,
                    d.Name,
                    d.Description,
                    d.DeviceType,
                    d.RoomId))
                .ToList();

            return devices;
        }

        public async Task<Guid> Create(Device device, CancellationToken cancellationToken = default)
        {
            var deviceEntity = new DeviceEntity
            {
                Id = device.Id,
                Name = device.Name,
                Description = device.Description,
                DeviceType = device.DeviceType,
                RoomId = device.RoomId
            };

            await _context.Devices.AddAsync(deviceEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return deviceEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name, string desc, DeviceType deviceType, Guid roomId, CancellationToken cancellationToken = default)
        {
            await _context.Devices
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(d => d.Name, d => name)
                    .SetProperty(d => d.Description, d => desc)
                    .SetProperty(d => d.DeviceType, d => deviceType)
                    .SetProperty(d => d.RoomId, d => roomId), cancellationToken);

            return id;
        }

        public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Devices
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return id;
        }
    }
}
