namespace Proget.Messaging.RabbitMq.Routing;

internal sealed class RabbitMqRoutingBuilder : IRabbitMqRoutingBuilder
{
    private class RabbitMqRoutingOptions
    {
        public RabbitMqExchangeType? ExchangeType { get; set; }
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

    public IRabbitMqRoutingBuilder SetExchange(Type type)
    {
        _rabbitMqRoutingOptions.Exchange = (GetAttribute(type)?.Exchange ?? _options.Exchange?.Name ?? type.Assembly.FullName)
            .Underscore();

        return this;
    }

    public IRabbitMqRoutingBuilder SetRoutingKey(Type type)
    {
        _rabbitMqRoutingOptions.RoutingKey = (GetAttribute(type)?.RoutingKey ?? type.Name)
            .Underscore();
        
        return this;
    }

    public IRabbitMqRoutingBuilder SetQueue(Type type)
    {
        _rabbitMqRoutingOptions.Queue = (GetAttribute(type)?.Queue ?? _options.Queue?.Name ?? $"{type.Assembly.FullName}.{type.Name}")
            .Underscore();
        
        return this;
    }

    public RabbitMqRouting Build(Type type)
    {
        var exchange = _rabbitMqRoutingOptions.Exchange
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.Exchange));
        
        var routingKey = _rabbitMqRoutingOptions.RoutingKey
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.RoutingKey));

        var queue = _rabbitMqRoutingOptions.Queue
            ?? throw new NullReferenceException(nameof(_rabbitMqRoutingOptions.Queue));
        
        return new RabbitMqRouting(type, exchange, routingKey, queue);
    }
        
    private static RabbitMqRoutingAttribute? GetAttribute(MemberInfo type)
        => type.GetCustomAttribute<RabbitMqRoutingAttribute>();
}
