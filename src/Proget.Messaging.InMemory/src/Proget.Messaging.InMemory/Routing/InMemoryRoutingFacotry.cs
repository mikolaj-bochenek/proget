namespace Proget.Messaging.InMemory.Routing;

internal sealed class InMemoryRoutingFactory : IInMemoryRoutingFactory
{
    private readonly IInMemoryRoutingBuilder _builder;

    public InMemoryRoutingFactory(IInMemoryRoutingBuilder builder)
    {
        _builder = builder;
    }

    public InMemoryRouting? Get<TMessage>() where TMessage : class, IMessage
        => Get(typeof(TMessage));

    public InMemoryRouting? Get(Type type)
    {
        var attribute = type.GetCustomAttribute<InMemoryRoutingAttribute>();
        if (attribute is null)
        {
            return null;
        }
        return _builder
            .SetExchangeType(attribute)
            .SetRoutingKey(attribute, type)
            .Build(type);
    }
}
