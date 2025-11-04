using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IDeviceRepository
{
    Task<Guid> Create(Device device);
    Task<Guid> Delete(Guid id);
    Task<List<Device>> Get();
    Task<Guid> Update(Guid id, string name, string desc, DeviceType deviceType, Guid roomId);
}