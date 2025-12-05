using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IDeviceRepository
{
    Task<Guid> Create(Device device, CancellationToken cancellationToken = default);
    Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default);
    Task<List<Device>> Get(CancellationToken cancellationToken = default);
    Task<Device> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> Update(Guid id, string name, string desc, DeviceType deviceType, Guid roomId, CancellationToken cancellationToken = default);
    Task<Guid> Update(Guid id, string name, string desc, DeviceType deviceType, bool isEnabled, Guid roomId, CancellationToken cancellationToken = default);
}