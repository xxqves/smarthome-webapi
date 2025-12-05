using SmHm.Contracts.Events.DeviceEvents;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Messaging;
using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageBus _messageBus;

        public DeviceService(IDeviceRepository repository, ICurrentUserService currentUserService, IMessageBus messageBus)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _messageBus = messageBus;
        }

        public async Task<List<Device>> GetAllDevices(CancellationToken cancellationToken = default)
        {
            return await _repository.Get(cancellationToken);
        }

        public async Task<Guid> CreateDevice(Device device, CancellationToken cancellationToken = default)
        {
            var @event = new DeviceCreated(
                device.Id,
                device.DeviceType,
                device.RoomId,
                _currentUserService.UserId,
                _currentUserService.UserName,
                DateTime.UtcNow);

            await _messageBus.PublishAsync(@event, cancellationToken);

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

        public async Task<Guid> TurnOn(Guid id, CancellationToken cancellationToken = default)
        {
            var device = await _repository.GetById(id, cancellationToken);

            if (device == null)
            {
                throw new InvalidOperationException("Device not found!");
            }

            device.TurnOn();

            await _repository.Update(
                device.Id,
                device.Name,
                device.Description,
                device.DeviceType,
                device.IsEnabled,
                device.RoomId,
                cancellationToken
                );

            return device.Id;
        }
    }
}
