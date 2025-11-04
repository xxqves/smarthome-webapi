using SmHm.Core.Enums;

namespace SmHm.Persistence.PostgreSql.Entities
{
    public class RoomEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public RoomType RoomType { get; set; }

        public int Floor { get; set; }

        public Guid UserId { get; set; }

        public List<DeviceEntity> Devices { get; set; } = [];

        public UserEntity? User { get; set; }
    }
}
