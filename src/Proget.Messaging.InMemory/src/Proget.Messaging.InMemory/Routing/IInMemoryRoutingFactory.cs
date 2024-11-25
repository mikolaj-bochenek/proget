namespace Proget.Messaging.InMemory.Routing;

internal interface IInMemoryRoutingFactory
{
    InMemoryRouting Get<TMessage>() where TMessage : class, IMessage;
    InMemoryRouting Get(Type type);
}
