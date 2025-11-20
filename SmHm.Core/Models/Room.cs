using SmHm.Core.Enums;

namespace SmHm.Core.Models
{
    public sealed class Room
    {
        public const int MAX_NAME_LENGTH = 50;

        public const int MAX_DESCRIPTION_LENGTH = 150;

        private Room(Guid id, string name, string desc, RoomType roomType, int floor, Guid userId, List<Device> devices)
        {
            Id = id;
            Name = name;
            Description = desc;
            RoomType = roomType;
            Floor = floor;
            UserId = userId;
            Devices = devices;
        }

        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public RoomType RoomType { get; }

        public int Floor { get; }

        public List<Device> Devices { get; } = [];

        public Guid UserId { get; }

        public static Room Create(Guid id, string name, string desc, RoomType roomType, int floor, Guid userId, List<Device> devices)
        {
            if (name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException($"Name length must not exceed {MAX_NAME_LENGTH} characters.");
            }

            if (desc.Length > MAX_DESCRIPTION_LENGTH)
            {
                throw new ArgumentException($"Description length must not exceed {MAX_DESCRIPTION_LENGTH} characters.");
            }

            return new Room(id, name, desc, roomType, floor, userId, devices);
        }
    }
}
