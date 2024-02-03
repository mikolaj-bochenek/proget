namespace Proget.Messaging.InMemory.Publishers;

internal sealed class InMemoryMessagePublisher : IMessagePublisherStrategy
{
    private readonly IMessageRoutingFactory _messageRoutingFactory;
    private readonly IMessageChannel _messageChannel;
    private readonly ISerializer _serializer;

    public InMemoryMessagePublisher(
        IMessageRoutingFactory messageRoutingFactory,
        IMessageChannel messageChannel,
        ISerializer serializer
    )
    {
        _messageRoutingFactory = messageRoutingFactory;
        _messageChannel = messageChannel;
        _serializer = serializer;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message, CancellationToken cancellationToken = default
    ) where TMessage : class, IMessage
    {
        var routing = _messageRoutingFactory.Get<TMessage>();
        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);
        var messageEnvelope = new MessageEnvelope(routing.Exchange, routing.RoutingKey, body);

        await _messageChannel.Writer.WriteAsync(messageEnvelope, cancellationToken);
    }
}