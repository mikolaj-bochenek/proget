namespace Proget.Messaging;

public interface ISubscriptionConsumer
{
    void Consume(IMessageSubscription messageSubscription, CancellationToken cancellationToken = default);
}