using SmHm.NotificationService.DTO;
using SmHm.NotificationService.Interfaces;
using System.Text.Json;

namespace SmHm.NotificationService.Services
{
    public class EmailNotificationHandler : INotificationHandler
    {
        private readonly ILogger<EmailNotificationHandler> _logger;

        public EmailNotificationHandler(ILogger<EmailNotificationHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(string message, CancellationToken cancellationToken = default)
        {
            try
            {
                var userEvent = JsonSerializer.Deserialize<UserRegisteredEvent>(message);

                _logger.LogInformation($"Получено сообщение: {userEvent}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка десереализации объекта");
            }
            
            return Task.CompletedTask;
        }
    }
}
