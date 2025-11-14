using RabbitMQ.Client;
using SmHm.Core.Abstractions.Messaging;
using System.Text;
using System.Text.Json;

namespace SmHm.Infrastructure.Messaging
{
    public class RabbitMqMessageBus : IRabbitMqMessageBus
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "user_registered";
        private readonly string _username = "rmuser";
        private readonly string _password = "rmpassword";

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
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
