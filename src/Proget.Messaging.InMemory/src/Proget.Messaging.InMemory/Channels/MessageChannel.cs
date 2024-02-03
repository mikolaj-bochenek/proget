namespace Proget.Messaging.InMemory.Channels;

internal sealed class MessageChannel : IMessageChannel
{
    private readonly Channel<MessageEnvelope> _channel
        = Channel.CreateUnbounded<MessageEnvelope>();

    public ChannelReader<MessageEnvelope> Reader => _channel.Reader;
    public ChannelWriter<MessageEnvelope> Writer => _channel.Writer;
}