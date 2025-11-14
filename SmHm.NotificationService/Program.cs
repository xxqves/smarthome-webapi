using SmHm.NotificationService.Interfaces;
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

                    services.AddSingleton<INotificationHandler, EmailNotificationHandler>();
                    services.AddHostedService<NotificationWorker>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}