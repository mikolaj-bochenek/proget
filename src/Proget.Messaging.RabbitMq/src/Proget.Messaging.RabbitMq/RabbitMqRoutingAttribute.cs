namespace Proget.Messaging.RabbitMq;

[AttributeUsage(AttributeTargets.Class)]
public class RabbitMqRoutingAttribute : Attribute
{
    public string? Exchange { get; }
    public string? RoutingKey { get; }
    public string? Queue { get; }

    public RabbitMqRoutingAttribute(
        string? exchange = null,
        string? routingKey = null,
        string? queue = null
    )
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Queue = queue;
    }
}
