namespace Proget.Messaging.RabbitMq.Publishers;

internal sealed class RabbitMqMessagePublisher : IMessagePublisherStrategy
{
    private readonly IModel _channel;
    private readonly IRabbitMqRoutingFactory _rabbitMqRoutingFactory;
    private readonly ISerializer _serializer;
    private readonly ILogger<RabbitMqSubscriptionConsumer> _logger;

    public RabbitMqMessagePublisher(
        IChannelFactory channelFactory,
        IRabbitMqRoutingFactory rabbitMqRoutingFactory,
        ISerializer serializer,
        ILogger<RabbitMqSubscriptionConsumer> logger
    )
    {
        _channel = channelFactory.Create();
        _rabbitMqRoutingFactory = rabbitMqRoutingFactory;
        _serializer = serializer;
        _logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        where TMessage : class, IMessage
    {
        var type = message.GetType();
        var routing = _rabbitMqRoutingFactory.Get(type);
        var exchange = routing.Exchange;
        var routingKey = routing.RoutingKey;
        var exchangeType = routing.ExchangeType.Humanize(LetterCasing.LowerCase);

        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);
        var properties = _channel.CreateBasicProperties();

        var persistent = false;
        var messageId = Guid.NewGuid().ToString("N");
        var correlationId = Guid.NewGuid().ToString("N");
        var timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        var headers = new Dictionary<string, object>();
        var basicAck = true;
        var complexAck = true;
        var declare = true;
        var durable = true;
        var autoDelete = false;
        var arguments = new Dictionary<string, object>();

        properties.Persistent = persistent;
        properties.MessageId = messageId;
        properties.CorrelationId = correlationId;
        properties.Timestamp = timestamp;
        properties.Headers = headers;

        if (basicAck)
        {
            _channel.ConfirmSelect();
            _channel.BasicAcks += (sender, eventArgs) =>
            {
                var logInfoMsg = string.Join(
                    Environment.NewLine,
                    "The Basic ACK has been returned:",
                    string.Format("messageId: '{0}'", messageId),
                    string.Format("correlationId: '{0}'", correlationId),
                    string.Format("timestamp: '{0}'", timestamp),
                    string.Format("routingKey: '{0}'", routingKey),
                    string.Format("exchange: '{0}'", exchange),
                    string.Format("payload: '{0}'", payload),
                    string.Format("receivedBy: '{0}'", "Publisher")
                );
                _logger.LogInformation("{Message}", logInfoMsg);
            };
        }

        if (complexAck)
        {
            _channel.BasicReturn += (sender, eventArgs) =>
            {
                var logMsg = string.Join(
                    Environment.NewLine,
                    "The Complex ACK has been returned:",
                    string.Format("messageId: '{0}'", messageId),
                    string.Format("correlationId: '{0}'", correlationId),
                    string.Format("replyCode: '{0}'", eventArgs.ReplyCode),
                    string.Format("replyText: '{0}'", eventArgs.ReplyText),
                    string.Format("routingKey: '{0}'", eventArgs.RoutingKey),
                    string.Format("exchange: '{0}'", eventArgs.Exchange),
                    string.Format("body: '{0}'", eventArgs.Body),
                    string.Format("receivedBy: '{0}'", "Publisher")
                );

                if (eventArgs.ReplyCode is 200)
                {
                    _logger.LogInformation("{Message}", logMsg);
                }
                else
                {
                    _logger.LogError("{Message}", logMsg);
                }
            };
        }

        if (declare)
        {
            _channel.ExchangeDeclare(
                exchange,
                exchangeType.Humanize(LetterCasing.LowerCase),
                durable,
                autoDelete,
                arguments
            );

            var logInfoMsg = string.Join(
                Environment.NewLine,
                "The Exchange has been declared:",
                string.Format("name: '{0}'", exchange),
                string.Format("type: '{0}'", exchangeType),
                string.Format("routing-key: '{0}'", routingKey),
                string.Format("durable: '{0}'", durable),
                string.Format("autoDelete: '{0}'", autoDelete),
                string.Format("declaredBy: '{0}'", "Publisher")
            );
            _logger.LogInformation("{Message}", logInfoMsg);
        }

        _channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: complexAck,
            basicProperties: properties,
            body: body
        );

        var logMsg = string.Join(
            Environment.NewLine,
            "The Message has been published:",
            string.Format("messageId: '{0}'", properties.MessageId),
            string.Format("correlationId: '{0}'", properties.CorrelationId),
            string.Format("timestamp: '{0}'", properties.CorrelationId),
            string.Format("exchange: '{0}'", exchange),
            string.Format("routing-key: '{0}'", routingKey),
            string.Format("publichedBy: '{0}'", "Publisher")
        );
        _logger.LogInformation("{Message}", logMsg);

        await Task.CompletedTask;
    }
}
