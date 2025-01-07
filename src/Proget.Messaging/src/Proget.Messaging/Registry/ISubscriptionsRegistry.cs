namespace Proget.Messaging.Registry;

internal interface ISubscriptionsRegistry
{
    IAsyncEnumerable<IMessageSubscription> ReadAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(IMessageSubscription subscription);
    void Add(IMessageSubscription subscription);
}