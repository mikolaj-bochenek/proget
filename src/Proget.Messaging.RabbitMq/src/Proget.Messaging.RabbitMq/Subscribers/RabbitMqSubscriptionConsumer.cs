namespace Proget.Messaging.RabbitMq.Subscribers;

internal sealed class RabbitMqSubscriptionConsumer : ISubscriptionConsumer
{
    private readonly IModel _channel;
    private readonly IRabbitMqRoutingFactory _rabbitMqRoutingFactory;
    private readonly ISerializer _serializer;
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqOptions _options;
    private readonly ILogger<RabbitMqSubscriptionConsumer> _logger;

    public RabbitMqSubscriptionConsumer(
        IChannelFactory channelFactory,
        IRabbitMqRoutingFactory rabbitMqRoutingFactory,
        ISerializer serializer,
        IServiceProvider serviceProvider,
        IOptions<RabbitMqOptions> options,
        ILogger<RabbitMqSubscriptionConsumer> logger
    )
    {
        _channel = channelFactory.Create();
        _rabbitMqRoutingFactory = rabbitMqRoutingFactory;
        _serializer = serializer;
        _serviceProvider = serviceProvider;
        _options = options.Value;
        _logger = logger;
    }

    public void Consume(IMessageSubscription messageSubscription, CancellationToken cancellationToken = default)
    {
        var type = messageSubscription.Type;
        var callback = messageSubscription.Callback;

        var routing = _rabbitMqRoutingFactory.Get(type);
        if (routing is null)
        {
            return;
        }
        
        var exchange = routing.Exchange;
        var routingKey = routing.RoutingKey;
        var queue = routing.Queue;
        var multipleAck = _options.Queue?.MultipleAck ?? false;
        var multipleNack = _options.Queue?.MultipleNack ?? false;
        var requeueRejected = _options.Queue?.RequeueRejected ?? false;
        var autoAck = _options.Queue?.AutoAck ?? false;

        if (_options.Queue?.Declare is true)
        {
            _options.Queue.Name = queue;
            _options.Queue.BindExchange = exchange;
            _options.Queue.BindRoutingKey = routingKey;
            RabbitMqQueue.DeclareQueue(_channel, _options.Queue, _logger);
        }

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            try
            {
                var messageId = eventArgs.BasicProperties.MessageId;
                var correlationId = eventArgs.BasicProperties.CorrelationId;
                var deliveryTag = eventArgs.DeliveryTag;
                var timestamp = eventArgs.BasicProperties.Timestamp.UnixTime;
                var payload = Encoding.UTF8.GetString(eventArgs.Body.Span);
                var message = _serializer.Deserialize(payload, type);
               
                var logInfoMsg = string.Format("Message: {0} with delivery-tag: {1} received from {2}", messageId, deliveryTag,  eventArgs.Exchange);
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