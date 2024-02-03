namespace Proget.Messaging.Subscriptions;

internal sealed record MessageSubscription(
    Type Type,
    Func<IServiceProvider, object, Task> Callback
) : IMessageSubscription;
