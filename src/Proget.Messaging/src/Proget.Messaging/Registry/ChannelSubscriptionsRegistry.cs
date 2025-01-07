namespace Proget.Messaging.Registry;

internal sealed class ChannelSubscriptionsRegistry : ISubscriptionsRegistry
{
    private readonly Channel<IMessageSubscription> _channel
        = Channel.CreateUnbounded<IMessageSubscription>();

    public async IAsyncEnumerable<IMessageSubscription> ReadAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (await _channel.Reader.WaitToReadAsync(cancellationToken))
        {
            while (_channel.Reader.TryRead(out var subscription))
            {
                yield return subscription;
            }
        }
    }

    public async Task AddAsync(IMessageSubscription subscription)
    {
        await _channel.Writer.WriteAsync(subscription);
    }

    public void Add(IMessageSubscription subscription)
    {
        _channel.Writer.TryWrite(subscription);
    }
}