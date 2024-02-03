namespace Proget.Messaging;

public interface IMessageSubscriber
{
    IMessageSubscriber Subscribe<TMessage>(Func<IServiceProvider, TMessage, Task> callback)
        where TMessage : class, IMessage; 
}