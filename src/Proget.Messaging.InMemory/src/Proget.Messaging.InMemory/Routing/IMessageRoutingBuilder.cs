namespace Proget.Messaging.InMemory.Routing;

internal interface IMessageRoutingBuilder
{
    IMessageRoutingBuilder SetRoutingKey(Type type);
    IMessageRoutingBuilder SetExchange(Type type);
    MessageRouting Build(Type type);
}