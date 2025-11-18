using MassTransit;
using SmHm.Contracts.Events.UserEvents;

namespace SmHm.NotificationService.Consumers
{
    public class UserLoggedInConsumer : IConsumer<UserLoggedIn>
    {
        private readonly ILogger<UserLoggedInConsumer> _logger;

        public UserLoggedInConsumer(ILogger<UserLoggedInConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<UserLoggedIn> context)
        {
            _logger.LogInformation($"Получено сообщение входа в систему: {context.Message}");

            return Task.CompletedTask;
        }
    }
}
