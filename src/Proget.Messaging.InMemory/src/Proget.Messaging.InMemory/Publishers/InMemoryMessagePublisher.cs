namespace Proget.Messaging.InMemory.Publishers;

internal sealed class InMemoryMessagePublisher : IMessagePublisherStrategy
{
    private readonly IInMemoryRoutingFactory _routingFactory;
    private readonly IMessageChannel _messageChannel;
    private readonly ISerializer _serializer;

    public InMemoryMessagePublisher(
        IInMemoryRoutingFactory routingFactory,
        IMessageChannel messageChannel,
        ISerializer serializer
    )
    {
        _routingFactory = routingFactory;
        _messageChannel = messageChannel;
        _serializer = serializer;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message, CancellationToken cancellationToken = default
    ) where TMessage : class, IMessage
    {
        var routing = _routingFactory.Get<TMessage>();
        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);
        var messageEnvelope = new MessageEnvelope(routing.ExchangeType, routing.RoutingKey, body);

        await _messageChannel.Writer.WriteAsync(messageEnvelope, cancellationToken);
    }
}