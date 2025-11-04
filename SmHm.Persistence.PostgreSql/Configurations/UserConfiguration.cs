using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmHm.Core.Models;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.UserName)
                .HasMaxLength(User.MAX_USERNAME_LENGTH)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder
                .HasMany(u => u.Rooms)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);
        }
    }
}
