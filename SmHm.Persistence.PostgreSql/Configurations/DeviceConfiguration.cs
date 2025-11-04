using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<DeviceEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Name)
                .HasMaxLength(Device.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(Device.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(d => d.DeviceType)
                .IsRequired();

            builder
                .HasOne(d => d.Room)
                .WithMany(r => r.Devices)
                .HasForeignKey(d => d.RoomId);
        }
    }
}
