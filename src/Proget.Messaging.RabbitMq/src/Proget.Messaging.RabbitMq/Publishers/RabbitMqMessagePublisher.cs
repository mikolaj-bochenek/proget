namespace Proget.Messaging.RabbitMq.Publishers;

internal sealed class RabbitMqMessagePublisher : IMessagePublisherStrategy
{
    private readonly IModel _channel;
    private readonly IRabbitMqRoutingFactory _rabbitMqRoutingFactory;
    private readonly ISerializer _serializer;
    private readonly ILogger<RabbitMqSubscriptionConsumer> _logger;
    private readonly RabbitMqOptions _options;

    public RabbitMqMessagePublisher(
        IChannelFactory channelFactory,
        IRabbitMqRoutingFactory rabbitMqRoutingFactory,
        ISerializer serializer,
        ILogger<RabbitMqSubscriptionConsumer> logger,
        IOptions<RabbitMqOptions> options
    )
    {
        _channel = channelFactory.Create();
        _rabbitMqRoutingFactory = rabbitMqRoutingFactory;
        _serializer = serializer;
        _logger = logger;
        _options = options.Value;

        if (_options.Channel is not null)
        {
            RabbitMqChannel.ConfigureChannel(_channel, _options.Channel, _logger);
        }

        if (_options.Exchange is not null)
        {
            RabbitMqExchange.DeclareExchange(_channel, _options.Exchange, _logger);
        }
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : class, IMessage
    {
        var type = message.GetType();
        var routing = _rabbitMqRoutingFactory.Get(type);
        var exchange = routing.Exchange;
        var routingKey = routing.RoutingKey;

        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);
        var properties = _channel.CreateBasicProperties();

        var persistence = false;
        var mandatory = true;
        var messageId = Guid.NewGuid().ToString("N");
        var correlationId = Guid.NewGuid().ToString("N");
        var timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        var headers = new Dictionary<string, object>();
       
        properties.Persistent = persistence;
        properties.MessageId = messageId;
        properties.CorrelationId = correlationId;
        properties.Timestamp = timestamp;
        properties.Headers = headers;

        _channel.BasicPublish(exchange, routingKey, mandatory, properties, body);

        if (_options.Logger)
        {
            var logMsg = string.Format("Message: {0} Published to exchange: {1}", messageId, exchange);
            _logger.LogInformation("{Message}", logMsg);
        }

        await Task.CompletedTask;
    }
}
