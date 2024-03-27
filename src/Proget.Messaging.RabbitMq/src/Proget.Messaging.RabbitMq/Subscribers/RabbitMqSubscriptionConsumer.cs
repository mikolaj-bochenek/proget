namespace Proget.Messaging.RabbitMq.Subscribers;

internal sealed class RabbitMqSubscriptionConsumer : ISubscriptionConsumer
{
    private readonly IModel _channel;
    private readonly IRabbitMqRoutingFactory _rabbitMqRoutingFactory;
    private readonly ISerializer _serializer;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMqSubscriptionConsumer> _logger;

    public RabbitMqSubscriptionConsumer(
        IChannelFactory channelFactory,
        IRabbitMqRoutingFactory rabbitMqRoutingFactory,
        ISerializer serializer,
        IServiceProvider serviceProvider,
        ILogger<RabbitMqSubscriptionConsumer> logger
    )
    {
        _channel = channelFactory.Create();
        _rabbitMqRoutingFactory = rabbitMqRoutingFactory;
        _serializer = serializer;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void Consume(IMessageSubscription messageSubscription, CancellationToken cancellationToken = default)
    {
        var type = messageSubscription.Type;
        var callback = messageSubscription.Callback;

        var routing = _rabbitMqRoutingFactory.Get(type);
        var exchange = routing.Exchange;
        var routingKey = routing.RoutingKey;
        var queue = routing.Queue;

        var declare = true;
        var durable = true; 
        var exclusive = false;
        var autoDelete = false;
        uint prefetchSize = 0;
        ushort prefetchCount = 0;
        var global = false;
        var autoAck = false;
        var multipleAck = true;
        var multipleNack = false;
        var requeueRejected = false;

        if (declare)
        {
            _channel.QueueDeclare(queue, durable, exclusive, autoDelete);

            var logInfoMsg = string.Join(
                Environment.NewLine,
                "The Queue has been declared:",
                string.Format("name: '{0}'", queue),
                string.Format("routing-key: '{0}'", routingKey),
                string.Format("exchange: '{0}'", exchange),
                string.Format("durable: '{0}'", durable),
                string.Format("exclusive: '{0}'", exclusive),
                string.Format("autoDelete: '{0}'", autoDelete),
                string.Format("declaredBy: '{0}'", "Subscriber")
            );
            _logger.LogInformation("{Message}", logInfoMsg);
        }

        _channel.QueueBind(queue, exchange, routingKey);
        _channel.BasicQos(prefetchSize, prefetchCount, global);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            try
            {
                var messageId = eventArgs.BasicProperties.MessageId;
                var correlationId = eventArgs.BasicProperties.CorrelationId;
                var timestamp = eventArgs.BasicProperties.Timestamp.UnixTime;
                var payload = Encoding.UTF8.GetString(eventArgs.Body.Span);
                var message = _serializer.Deserialize(payload, type);

                var logInfoMsg = string.Join(
                    Environment.NewLine,
                    "The Message has been received:",
                    string.Format("messageId: '{0}'", messageId),
                    string.Format("correlationId: '{0}'", correlationId),
                    string.Format("timestamp: '{0}'", timestamp),
                    string.Format("queue: '{0}'", queue),
                    string.Format("routingKey: '{0}'", routingKey),
                    string.Format("exchange: '{0}'", exchange),
                    string.Format("payload: '{0}'", payload),
                    string.Format("receivedBy: '{0}'", "Subscriber")
                );
                _logger.LogInformation("{Message}", logInfoMsg);

                if (message is not null)
                {
                    await callback(_serviceProvider, message);
                }
                _channel.BasicAck(eventArgs.DeliveryTag, multipleAck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                
                if (multipleNack)
                {
                    _channel.BasicNack(eventArgs.DeliveryTag, multipleNack, requeueRejected);
                }
                _channel.BasicReject(eventArgs.DeliveryTag, requeueRejected);

                await Task.Yield();
                throw;
            }
        };

        _channel.BasicConsume(queue, autoAck, consumer);
    }
}