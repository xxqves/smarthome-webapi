using SmHm.Core.Enums;

namespace SmHm.Persistence.PostgreSql.Entities
{
    public class DeviceEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DeviceType DeviceType { get; set; }

        public bool IsEnabled { get; set; } = false;

        public Guid RoomId { get; set; }

        public RoomEntity? Room { get; set; }
    }
}
