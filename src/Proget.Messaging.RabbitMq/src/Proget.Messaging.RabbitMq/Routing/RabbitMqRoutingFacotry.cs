namespace Proget.Messaging.RabbitMq.Routing;

internal sealed class RabbitMqRoutingFactory : IRabbitMqRoutingFactory
{
    private readonly IRabbitMqRoutingBuilder _builder;

    public RabbitMqRoutingFactory(IRabbitMqRoutingBuilder builder)
    {
        _builder = builder;
    }

    public RabbitMqRouting Get<TMessage>() where TMessage : class, IMessage
        => Get(typeof(TMessage));

    public RabbitMqRouting Get(Type type)
        => _builder
            .SetExchangeType(type)
            .SetExchange(type)
            .SetRoutingKey(type)
            .SetQueue(type)
            .Build(type);
}
