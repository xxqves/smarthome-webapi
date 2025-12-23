using SmHm.Core.Abstractions.Messaging;

namespace SmHm.IntegrationTests.FakeDependencies
{
    public class FakeMessageBus : IMessageBus
    {
        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
