using MassTransit;
using SmHm.Contracts.Events.RoomEvents;

namespace SmHm.NotificationService.Consumers.Rooms
{
    public class RoomCreatedConsumer : IConsumer<RoomCreated>
    {
        private readonly ILogger<RoomCreatedConsumer> _logger;

        public RoomCreatedConsumer(ILogger<RoomCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<RoomCreated> context)
        {
            _logger.LogInformation($"Получено сообщение регистрации комнаты: {context.Message}");

            return Task.CompletedTask;
        }
    }
}
