namespace Proget.Messaging.InMemory.Subscribers;

internal sealed class InMemorySubscriptionConsumer : ISubscriptionConsumer
{
    private readonly IMessageRoutingFactory _messageRoutingFactory;
    private readonly IMessageListener _messageListener;
    private readonly ISerializer _serializer;
    private readonly IServiceProvider _serviceProvider;

    public InMemorySubscriptionConsumer(
        IMessageRoutingFactory messageRoutingFactory,
        IMessageListener messageListener,
        ISerializer serializer,
        IServiceProvider serviceProvider
    )
    {
        _messageRoutingFactory = messageRoutingFactory;
        _messageListener = messageListener;
        _serializer = serializer;
        _serviceProvider = serviceProvider;
    }

    public void Consume(IMessageSubscription messageSubscription, CancellationToken cancellationToken = default)
    {
        var type = messageSubscription.Type;
        var callback = messageSubscription.Callback;
        var routing = _messageRoutingFactory.Get(type);

        _messageListener.OnMessageReceived += async (messageEnvelope) =>
        {
            var exchange = messageEnvelope.Exchange;
            var routingKey = messageEnvelope.RoutingKey;
            var payload = Encoding.UTF8.GetString(messageEnvelope.Body);
            var message = _serializer.Deserialize(payload, type)
                ?? throw new InvalidOperationException("Deserialized message is null");

            await (exchange switch
            {
                ExchangeTypes.Fanout
                    => callback(_serviceProvider, message),
                ExchangeTypes.Direct when routingKey.Equals(routing.RoutingKey)
                    => callback(_serviceProvider, message),
                _ => throw new InvalidOperationException("The type of exchange has to be one of these: ['direct', 'fanout']"),
            });
        };
    }
}