namespace Proget.Messaging.InMemory.Listeners;

internal sealed class MessageListener : IMessageListener
{
    public event Func<MessageEnvelope, Task>? OnMessageReceived;

    public void SubmitMessage(MessageEnvelope messageEnvelope)
        => OnMessageReceived?.Invoke(messageEnvelope);
}