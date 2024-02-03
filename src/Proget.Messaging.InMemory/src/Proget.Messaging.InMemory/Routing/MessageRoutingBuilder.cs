namespace Proget.Messaging.InMemory.Routing;

internal sealed class MessageRoutingBuilder : IMessageRoutingBuilder
{
    private class MessageRoutingOptions
    {
        public ExchangeTypes? Exchange { get; set; }
        public string? RoutingKey { get; set; }
    }

    private readonly MessageRoutingOptions _messageRoutingOptions;
    private readonly InMemoryOptions _options;

    public MessageRoutingBuilder(InMemoryOptions options)
    {
        _options = options;
        _messageRoutingOptions = new MessageRoutingOptions();
    }

    public IMessageRoutingBuilder SetExchange(Type type)
    {
        _messageRoutingOptions.Exchange = GetAttribute(type)?.Exchange ?? _options.Exchange ?? ExchangeTypes.Fanout;
        return this;
    }

    public IMessageRoutingBuilder SetRoutingKey(Type type)
    {
        _messageRoutingOptions.RoutingKey = (GetAttribute(type)?.RoutingKey ?? _options.RoutingKey ?? type.Name)
            .Underscore();
        
        return this;
    }

    public MessageRouting Build(Type type)
    {
        var exchange = _messageRoutingOptions.Exchange
            ?? throw new ArgumentNullException(nameof(_messageRoutingOptions.Exchange));
        
        var routingKey = _messageRoutingOptions.RoutingKey
            ?? throw new ArgumentNullException(nameof(_messageRoutingOptions.RoutingKey));
        
        return new MessageRouting(type, exchange, routingKey);
    }
        
    private static MessageRoutingAttribute? GetAttribute(MemberInfo type)
        => type.GetCustomAttribute<MessageRoutingAttribute>();
}
