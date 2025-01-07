namespace Proget.Messaging.RabbitMq.Routing;

internal sealed class RabbitMqRoutingFactory : IRabbitMqRoutingFactory
{
    private readonly IRabbitMqRoutingBuilder _builder;

    public RabbitMqRoutingFactory(IRabbitMqRoutingBuilder builder)
    {
        _builder = builder;
    }

    public RabbitMqRouting? Get<TMessage>() where TMessage : class, IMessage
        => Get(typeof(TMessage));

    public RabbitMqRouting? Get(Type type)
    {
        var attribute = type.GetCustomAttribute<RabbitMqRoutingAttribute>();
        if (attribute is null)
        {
            return null;
        }

        return _builder
            .SetExchange(attribute, type)
            .SetRoutingKey(attribute, type)
            .SetQueue(attribute, type)
            .Build(type);
    }
}
