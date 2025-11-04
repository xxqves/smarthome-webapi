using Microsoft.EntityFrameworkCore;
using SmHm.Application.Services;
using SmHm.Core.Abstractions;
using SmHm.Persistence.PostgreSql;
using SmHm.Persistence.PostgreSql.Repositories;

namespace SmHm.WebApi.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSmHmDbContext(configuration);

            services.AddAbstractions();

            services.AddControllers();

            return services.AddSwaggerGen();
        }

        private static IServiceCollection AddSmHmDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<SmartHomeDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(SmartHomeDbContext)));
            });
        }

        private static IServiceCollection AddAbstractions(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}
