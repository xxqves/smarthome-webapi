using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmHm.Application.Services;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Auth;
using SmHm.Core.Abstractions.Messaging;
using SmHm.Infrastructure.Authentication;
using SmHm.Infrastructure.Messaging;
using SmHm.Persistence.PostgreSql;
using SmHm.Persistence.PostgreSql.Repositories;
using System.Text;

namespace SmHm.WebApi.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(RabbitMqOptions)));

            services.AddApiAuthentication();

            services.AddSmHmDbContext(configuration);

            services.AddAbstractions();

            services.AddControllers();

            services.AddHttpContextAccessor();

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
            services.AddSingleton<IRabbitMqMessageBus, RabbitMqMessageBus>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

        private static IServiceCollection AddApiAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jwt-service-staff"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
