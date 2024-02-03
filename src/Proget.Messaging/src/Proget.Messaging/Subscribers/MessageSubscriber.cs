namespace Proget.Messaging.Subscribers;

internal sealed class MessageSubscriber : IMessageSubscriber
{
    private readonly ISubscribersChannel _subscribersChannel;

    public MessageSubscriber(ISubscribersChannel subscribersChannel)
    {
        _subscribersChannel = subscribersChannel;
    }

    public IMessageSubscriber Subscribe<TMessage>(Func<IServiceProvider, TMessage, Task> callback)
        where TMessage : class, IMessage
    {
        var type = typeof(TMessage);
        var subscription = new MessageSubscription(type, (sp, msg) => callback(sp, (TMessage)msg));

        _subscribersChannel.Writer.TryWrite(subscription);

        return this;
    }
}
