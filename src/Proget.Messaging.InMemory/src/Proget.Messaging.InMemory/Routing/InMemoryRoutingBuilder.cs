namespace Proget.Messaging.InMemory.Routing;

internal sealed class InMemoryRoutingBuilder : IInMemoryRoutingBuilder
{
    private class MessageRoutingOptions
    {
        public InMemoryExchangeType? Exchange { get; set; }
        public string? RoutingKey { get; set; }
    }

    private readonly MessageRoutingOptions _messageRoutingOptions;
    private readonly InMemoryOptions _options;

    public InMemoryRoutingBuilder(InMemoryOptions options)
    {
        _options = options;
        _messageRoutingOptions = new MessageRoutingOptions();
    }

    public IInMemoryRoutingBuilder SetExchangeType(Type type)
    {
        _messageRoutingOptions.Exchange = GetAttribute(type)?.ExchangeType ?? _options.ExchangeType;
        return this;
    }

    public IInMemoryRoutingBuilder SetRoutingKey(Type type)
    {
        _messageRoutingOptions.RoutingKey = (GetAttribute(type)?.RoutingKey ?? _options.RoutingKey ?? type.Name)
            .Underscore();
        
        return this;
    }

    public InMemoryRouting Build(Type type)
    {
        var exchange = _messageRoutingOptions.Exchange
            ?? throw new ArgumentNullException(nameof(_messageRoutingOptions.Exchange));
        
        var routingKey = _messageRoutingOptions.RoutingKey
            ?? throw new ArgumentNullException(nameof(_messageRoutingOptions.RoutingKey));
        
        return new InMemoryRouting(type, exchange, routingKey);
    }
        
    private static InMemoryRoutingAttribute? GetAttribute(MemberInfo type)
        => type.GetCustomAttribute<InMemoryRoutingAttribute>();
}
