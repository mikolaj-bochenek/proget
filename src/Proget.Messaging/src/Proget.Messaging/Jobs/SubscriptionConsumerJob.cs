namespace Proget.Messaging.Jobs;

internal sealed class SubscriptionConsumerJob : BackgroundService
{
    private readonly ISubscriptionsRegistry _registry;
    private readonly IEnumerable<ISubscriptionConsumer> _subscriptionConsumers;

    public SubscriptionConsumerJob(
        ISubscriptionsRegistry registry,
        IEnumerable<ISubscriptionConsumer> subscriptionConsumers
    )
    {
        _registry = registry;
        _subscriptionConsumers = subscriptionConsumers;            
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await foreach (var subscription in _registry.ReadAllAsync(cancellationToken))
        {
            foreach (var consumer in _subscriptionConsumers)
            {
                consumer.Consume(subscription, cancellationToken);
            }
        }
    }
}