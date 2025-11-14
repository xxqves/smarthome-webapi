using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SmHm.NotificationService.DTO;
using SmHm.NotificationService.Interfaces;
using System.Text;
using System.Text.Json;

namespace SmHm.NotificationService
{
    public class NotificationWorker : BackgroundService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "user_registered";
        private readonly string _username = "rmuser";
        private readonly string _password = "rmpassword";
        private readonly ILogger<NotificationWorker> _logger;
        private readonly INotificationHandler _notificationHandler;

        public NotificationWorker(ILogger<NotificationWorker> logger, INotificationHandler notificationHandler)
        {
            _logger = logger;
            _notificationHandler = notificationHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            using var connection = await factory.CreateConnectionAsync(stoppingToken);

            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                try
                {
                    await _notificationHandler.HandleAsync(message, stoppingToken);

                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обработке сообщения");
                }
            };

            await channel.BasicConsumeAsync(_queueName, autoAck: false, consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
