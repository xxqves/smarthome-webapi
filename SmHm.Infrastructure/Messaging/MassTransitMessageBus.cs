using MassTransit;
using SmHm.Core.Abstractions.Messaging;

namespace SmHm.Infrastructure.Messaging
{
    public class MassTransitMessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitMessageBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
            where T : class
        {
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
