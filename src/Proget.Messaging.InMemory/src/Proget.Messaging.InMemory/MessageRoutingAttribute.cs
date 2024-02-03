namespace Proget.Messaging.InMemory;

[AttributeUsage(AttributeTargets.Class)]
public class MessageRoutingAttribute : Attribute
{
    public ExchangeTypes? Exchange { get; }
    public string? RoutingKey { get; }

    public MessageRoutingAttribute(ExchangeTypes exchange, string? routingKey = null)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
    }
}
