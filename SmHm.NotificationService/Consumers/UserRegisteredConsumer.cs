using MassTransit;
using SmHm.Contracts.Events.UserEvents;

namespace SmHm.NotificationService.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly ILogger<UserRegisteredConsumer> _logger;

        public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<UserRegistered> context)
        {
            _logger.LogInformation($"Получено сообщение регистрации: {context.Message}");

            return Task.CompletedTask;
        }
    }
}
