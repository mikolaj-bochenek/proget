namespace Proget.Messaging.InMemory.Subscribers;

internal sealed class InMemorySubscriptionConsumer : ISubscriptionConsumer
{
    private readonly IInMemoryRoutingFactory _messageRoutingFactory;
    private readonly IMessageListener _messageListener;
    private readonly ISerializer _serializer;
    private readonly IServiceProvider _serviceProvider;

    public InMemorySubscriptionConsumer(
        IInMemoryRoutingFactory messageRoutingFactory,
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
            var exchangeType = messageEnvelope.ExchangeType;
            var routingKey = messageEnvelope.RoutingKey;

            var shouldProcess = exchangeType switch
            {
                InMemoryExchangeType.Fanout => true,
                InMemoryExchangeType.Direct when routingKey.Equals(routing.RoutingKey) => true,
                _ => false
            };

            if (!shouldProcess)
            {
                return;
            }

            var payload = Encoding.UTF8.GetString(messageEnvelope.Body);
            var message = _serializer.Deserialize(payload, type)
                ?? throw new InvalidOperationException("Deserialized message is null");
            await callback(_serviceProvider, message);
        };
    }
}