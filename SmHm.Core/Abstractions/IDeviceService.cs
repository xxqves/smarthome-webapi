using SmHm.Core.Enums;
using SmHm.Core.Models;

namespace SmHm.Core.Abstractions;

public interface IDeviceService
{
    Task<Guid> CreateDevice(Device device);
    Task<Guid> DeleteDevice(Guid id);
    Task<List<Device>> GetAllDevices();
    Task<Guid> UpdateDevice(Guid id, string name, string desc, DeviceType deviceType, Guid roomId);
}