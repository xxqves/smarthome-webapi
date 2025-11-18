namespace SmHm.Core.Abstractions.Messaging
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class;
    }
}
