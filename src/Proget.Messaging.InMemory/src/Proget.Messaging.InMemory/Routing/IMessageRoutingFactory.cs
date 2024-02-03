namespace Proget.Messaging.InMemory.Routing;

internal interface IMessageRoutingFactory
{
    MessageRouting Get<TMessage>() where TMessage : class, IMessage;
    MessageRouting Get(Type type);
}
