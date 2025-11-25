using MassTransit;
using SmHm.Contracts.Events.DeviceEvents;

namespace SmHm.NotificationService.Consumers.Devices
{
    public class DeviceCreatedConsumer : IConsumer<DeviceCreated>
    {
        private readonly ILogger<DeviceCreatedConsumer> _logger;

        public DeviceCreatedConsumer(ILogger<DeviceCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<DeviceCreated> context)
        {
            _logger.LogInformation($"Получено сообщение регистрации устройства: {context.Message}");

            return Task.CompletedTask;
        }
    }
}
