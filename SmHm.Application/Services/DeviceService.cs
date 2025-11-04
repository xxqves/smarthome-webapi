using SmHm.Core.Abstractions;
using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;

        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Device>> GetAllDevices()
        {
            return await _repository.Get();
        }

        public async Task<Guid> CreateDevice(Device device)
        {
            return await _repository.Create(device);
        }

        public async Task<Guid> UpdateDevice(Guid id, string name, string desc, DeviceType deviceType, Guid roomId)
        {
            return await _repository.Update(id, name, desc, deviceType, roomId);
        }

        public async Task<Guid> DeleteDevice(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}
