namespace Proget.Messaging.RabbitMq;

[AttributeUsage(AttributeTargets.Class)]
public class RabbitMqRoutingAttribute : Attribute
{
    public ExchangeTypes ExchangeType { get; }
    public string? Exchange { get; }
    public string? RoutingKey { get; }
    public string? Queue { get; }

    public RabbitMqRoutingAttribute(
        ExchangeTypes exchangeType = ExchangeTypes.Fanout,
        string? exchange = null,
        string? routingKey = null,
        string? queue = null
    )
    {
        ExchangeType = exchangeType;
        Exchange = exchange;
        RoutingKey = routingKey;
        Queue = queue;
    }
}
