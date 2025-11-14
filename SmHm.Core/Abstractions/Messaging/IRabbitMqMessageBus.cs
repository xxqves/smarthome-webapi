
namespace SmHm.Core.Abstractions.Messaging
{
    public interface IRabbitMqMessageBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
    }
}