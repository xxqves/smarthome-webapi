using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
    {
        public void Configure(EntityTypeBuilder<RoomEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(Room.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(Room.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(r => r.RoomType)
                .IsRequired();

            builder
                .HasMany(r => r.Devices)
                .WithOne(d => d.Room)
                .HasForeignKey(d => d.RoomId);
        }
    }
}
