namespace Proget.Messaging.Jobs;

internal sealed class SubscriptionConsumerJob : BackgroundService
{
    private readonly ISubscribersChannel _subscribersChannel;
    private readonly IEnumerable<ISubscriptionConsumer> _subscriptionConsumers;

    public SubscriptionConsumerJob(
        ISubscribersChannel subscribersChannel,
        IEnumerable<ISubscriptionConsumer> subscriptionConsumers
    )
    {
        _subscribersChannel = subscribersChannel;
        _subscriptionConsumers = subscriptionConsumers;            
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await foreach (var subscription in _subscribersChannel.Reader.ReadAllAsync(cancellationToken))
        {
            foreach (var consumer in _subscriptionConsumers)
            {
                consumer.Consume(subscription, cancellationToken);
            }
        }
    }
}