using SmHm.Core.Abstractions;
using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        private readonly IRoomService _roomService;

        public DeviceService(IDeviceRepository repository, IRoomService roomService)
        {
            _repository = repository;
            _roomService = roomService;
        }

        public async Task<List<Device>> GetAllDevices(CancellationToken cancellationToken = default)
        {
            return await _repository.Get(cancellationToken);
        }

        public async Task<Guid> CreateDevice(Device device, CancellationToken cancellationToken = default)
        {
            return await _repository.Create(device, cancellationToken);
        }

        public async Task<Guid> UpdateDevice(Guid id, string name, string desc, DeviceType deviceType, Guid roomId, CancellationToken cancellationToken = default)
        {
            return await _repository.Update(id, name, desc, deviceType, roomId, cancellationToken);
        }

        public async Task<Guid> DeleteDevice(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.Delete(id, cancellationToken);
        }
    }
}
