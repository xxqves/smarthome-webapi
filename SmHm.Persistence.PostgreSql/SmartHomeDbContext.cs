using Microsoft.EntityFrameworkCore;
using SmHm.Persistence.PostgreSql.Configurations;
using SmHm.Persistence.PostgreSql.Entities;

namespace SmHm.Persistence.PostgreSql
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options)
            : base(options)
        {
             
        }

        public DbSet<RoomEntity> Rooms { get; set; }

        public DbSet<DeviceEntity> Devices { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
