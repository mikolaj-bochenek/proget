namespace Proget.Messaging.Publishers;

internal sealed class MessagePublisher : IMessagePublisher
{
    private readonly IEnumerable<IMessagePublisherStrategy> _strategies;

    public MessagePublisher(IEnumerable<IMessagePublisherStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : class, IMessage
    {
        foreach (var strategy in _strategies)
        {
            await strategy.PublishAsync(message, cancellationToken);
        }
    }
}
