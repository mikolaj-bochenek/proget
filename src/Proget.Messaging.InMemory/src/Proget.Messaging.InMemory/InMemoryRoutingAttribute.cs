namespace Proget.Messaging.InMemory;

[AttributeUsage(AttributeTargets.Class)]
public class InMemoryRoutingAttribute : Attribute
{
    public InMemoryExchangeType? ExchangeType { get; }
    public string? RoutingKey { get; }

    public InMemoryRoutingAttribute(InMemoryExchangeType exchangeType, string? routingKey = null)
    {
        ExchangeType = exchangeType;
        RoutingKey = routingKey;
    }
}
