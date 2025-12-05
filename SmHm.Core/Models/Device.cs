using SmHm.Core.Enums;
using System.Runtime.InteropServices;

namespace SmHm.Core.Models
{
    public sealed class Device
    {
        public const int MAX_NAME_LENGTH = 50;

        public const int MAX_DESCRIPTION_LENGTH = 150;

        private Device(Guid id, string name, string desc, DeviceType deviceType, Guid roomId)
        {
            Id = id;
            Name = name;
            Description = desc;
            DeviceType = deviceType;
            RoomId = roomId;
        }

        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public DeviceType DeviceType { get; }

        public bool IsEnabled { get; private set; } = false;

        public Guid RoomId { get; }

        public static Device Create(Guid id, string name, string desc, DeviceType deviceType, Guid roomId)
        {
            if (name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException($"Name length must not exceed {MAX_NAME_LENGTH} characters.");
            }

            if (desc.Length > MAX_DESCRIPTION_LENGTH)
            {
                throw new ArgumentException($"Description length must not exceed {MAX_DESCRIPTION_LENGTH} characters.");
            }

            var device = new Device(id, name, desc, deviceType, roomId);

            return device;
        }

        public void TurnOn()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
            }
        }
    }
}
