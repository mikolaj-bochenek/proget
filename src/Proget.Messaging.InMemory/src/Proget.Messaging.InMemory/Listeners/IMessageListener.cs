namespace Proget.Messaging.InMemory.Listeners;

internal interface IMessageListener
{
    event Func<MessageEnvelope, Task> OnMessageReceived;

    void SubmitMessage(MessageEnvelope messageEnvelope);
}
