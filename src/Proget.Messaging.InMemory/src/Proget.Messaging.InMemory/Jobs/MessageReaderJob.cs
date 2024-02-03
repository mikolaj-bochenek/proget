namespace Proget.Messaging.InMemory.Jobs;

internal sealed class MessageReaderJob : BackgroundService
{
    private readonly IMessageListener _messageListener;
    private readonly IMessageChannel _messageChannel;

    public MessageReaderJob(IMessageListener messageListener, IMessageChannel messageChannel)
    {
        _messageListener = messageListener;
        _messageChannel = messageChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await foreach (var messageEnvelope in _messageChannel.Reader.ReadAllAsync(cancellationToken))
        {
            _messageListener.SubmitMessage(messageEnvelope);
        }
    }
}