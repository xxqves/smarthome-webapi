using MassTransit;
using Microsoft.Extensions.Options;
using SmHm.NotificationService.Consumers;
using SmHm.NotificationService.Messaging;

namespace SmHm.NotificationService.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, HostBuilderContext context)
        {
            return services.AddMassTransitRabbitMq(context);
        }

        private static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services, HostBuilderContext context) 
        {
            IConfiguration configuration = context.Configuration;

            services.Configure<RabbitMqOptions>(configuration!.GetSection(nameof(RabbitMqOptions)));

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<UserRegisteredConsumer>();

                cfg.AddConsumer<UserLoggedInConsumer>();

                cfg.UsingRabbitMq((context, bus) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                    bus.Host(options.HostName, "/", h =>
                    {
                        h.Username(options.UserName);
                        h.Password(options.Password);
                    });

                    bus.ReceiveEndpoint("notification_user_registered_queue", e =>
                    {
                        e.ConfigureConsumer<UserRegisteredConsumer>(context);
                    });

                    bus.ReceiveEndpoint("notification_user_logged_queue", e =>
                    {
                        e.ConfigureConsumer<UserLoggedInConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
