using SmHm.Core.Enums;

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

        public bool IsEnabled { get; } = false;

        public Guid RoomId { get; }

        public static Device Create(Guid id, string name, string desc, DeviceType deviceType, Guid roomId)
        {
            if (name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException("Name length can not be more than 50 characters");
            }

            if (desc.Length > MAX_DESCRIPTION_LENGTH)
            {
                throw new ArgumentException("Description length can not be more than 150 characters");
            }

            var device = new Device(id, name, desc, deviceType, roomId);

            return device;
        }
    }
}
