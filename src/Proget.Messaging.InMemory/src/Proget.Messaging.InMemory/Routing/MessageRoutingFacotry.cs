namespace Proget.Messaging.InMemory.Routing;

internal sealed class MessageRoutingFactory : IMessageRoutingFactory
{
    private readonly IMessageRoutingBuilder _builder;

    public MessageRoutingFactory(IMessageRoutingBuilder builder)
    {
        _builder = builder;
    }

    public MessageRouting Get<TMessage>() where TMessage : class, IMessage
        => Get(typeof(TMessage));

    public MessageRouting Get(Type type)
        => _builder
            .SetExchange(type)
            .SetRoutingKey(type)
            .Build(type);
}
