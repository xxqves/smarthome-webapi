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
            var roomId = context.Message.RoomId;
            var roomType = context.Message.RoomType;
            var userId = context.Message.UserId;
            var userName = context.Message.UserName;

            _logger.LogInformation($"Получено сообщение регистрации комнаты: " +
                $"Пользователь с ID {userId} и именем {userName} добавил комнату в личный кабинет." +
                $" ID комнаты: {roomId}, тип комнаты: {roomType}");

            return Task.CompletedTask;
        }
    }
}
