using MassTransit;
using Microsoft.Extensions.Options;
using SmHm.NotificationService.Consumers;
using SmHm.NotificationService.Services;

namespace SmHm.NotificationService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    IConfiguration configuration = context.Configuration;

                    services.Configure<RabbitMqOptions>(configuration!.GetSection(nameof(RabbitMqOptions)));

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<UserRegisteredConsumer>();

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
                        });
                    });
                })
                .Build();

            await host.RunAsync();
        }
    }
}