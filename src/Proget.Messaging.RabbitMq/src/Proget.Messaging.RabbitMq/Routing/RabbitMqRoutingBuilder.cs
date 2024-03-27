namespace Proget.Messaging.RabbitMq.Routing;

internal sealed class RabbitMqRoutingBuilder : IRabbitMqRoutingBuilder
{
    private class RabbitMqRoutingOptions
    {
        public ExchangeTypes? ExchangeType { get; set; }
        public string? Exchange { get; set; }
        public string? RoutingKey { get; set; }
        public string? Queue { get; set; }
    }

    private readonly RabbitMqRoutingOptions _rabbitMqRoutingOptions;
    private readonly RabbitMqOptions _options;

    public RabbitMqRoutingBuilder(RabbitMqOptions options)
    {
        _options = options;
        _rabbitMqRoutingOptions = new RabbitMqRoutingOptions();
    }

    public IRabbitMqRoutingBuilder SetExchangeType(Type type)
    {
        _rabbitMqRoutingOptions.ExchangeType = GetAttribute(type)?.ExchangeType ?? _options.ExchangeType ?? ExchangeTypes.Fanout;
        return this;
    }

    public IRabbitMqRoutingBuilder SetExchange(Type type)
    {
        _rabbitMqRoutingOptions.Exchange = (GetAttribute(type)?.Exchange ?? _options.Exchange ?? type.Assembly.FullName)
            .Underscore();

        return this;
    }

    public IRabbitMqRoutingBuilder SetRoutingKey(Type type)
    {
        _rabbitMqRoutingOptions.RoutingKey = (GetAttribute(type)?.RoutingKey ?? _options.RoutingKey ?? type.Name)
            .Underscore();
        
        return this;
    }

    public IRabbitMqRoutingBuilder SetQueue(Type type)
    {
        _rabbitMqRoutingOptions.Queue = (GetAttribute(type)?.Queue ?? _options.Queue ?? $"{type.Assembly.FullName}.{type.Name}")
            .Underscore();
        
        return this;
    }

    public RabbitMqRouting Build(Type type)
    {
        var exchangeType = _rabbitMqRoutingOptions.ExchangeType
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.ExchangeType));
        
        var exchange = _rabbitMqRoutingOptions.Exchange
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.Exchange));
        
        var routingKey = _rabbitMqRoutingOptions.RoutingKey
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.RoutingKey));

        var queue = _rabbitMqRoutingOptions.Queue
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.Queue));
        
        return new RabbitMqRouting(type, exchangeType, exchange, routingKey, queue);
    }
        
    private static RabbitMqRoutingAttribute? GetAttribute(MemberInfo type)
        => type.GetCustomAttribute<RabbitMqRoutingAttribute>();
}
