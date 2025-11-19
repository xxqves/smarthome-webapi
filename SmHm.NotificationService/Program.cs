using SmHm.NotificationService.Configuration;

namespace SmHm.NotificationService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddConfiguration(context);
                })
                .Build();

            await host.RunAsync();
        }
    }
}