namespace Proget.Messaging.Subscribers;

internal sealed class MessageSubscriber : IMessageSubscriber
{
    private readonly ISubscriptionsRegistry _registry;

    public MessageSubscriber(ISubscriptionsRegistry registry)
    {
        _registry = registry;
    }

    public IMessageSubscriber Subscribe<TMessage>(Func<IServiceProvider, TMessage, Task> callback)
        where TMessage : class, IMessage
    {
        var type = typeof(TMessage);
        var subscription = new MessageSubscription(type, (sp, msg) => callback(sp, (TMessage)msg));

        _registry.Add(subscription);

        return this;
    }
}
