using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IDeviceService
{
    Task<Guid> CreateDevice(Device device, CancellationToken cancellationToken = default);
    Task<Guid> DeleteDevice(Guid id, CancellationToken cancellationToken = default);
    Task<List<Device>> GetAllDevices(CancellationToken cancellationToken = default);
    Task<Guid> UpdateDevice(Guid id, string name, string desc, DeviceType deviceType, Guid roomId, CancellationToken cancellationToken = default);
    Task<Guid> TurnOn(Guid id, CancellationToken cancellationToken = default);
}