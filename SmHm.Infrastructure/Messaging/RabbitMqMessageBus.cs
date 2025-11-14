using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SmHm.Core.Abstractions.Messaging;
using System.Text;
using System.Text.Json;

namespace SmHm.Infrastructure.Messaging
{
    public class RabbitMqMessageBus : IRabbitMqMessageBus
    {
        private readonly string _queueName = "user_registered";
        private readonly RabbitMqOptions _options;

        public RabbitMqMessageBus(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password
            };

            using var connection = await factory.CreateConnectionAsync(cancellationToken);

            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: _queueName,
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true },
                body: body,
                cancellationToken);
        }
    }
}
