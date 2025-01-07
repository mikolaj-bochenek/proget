namespace Proget.Messaging.InMemory.Routing;

internal interface IInMemoryRoutingBuilder
{
    IInMemoryRoutingBuilder SetRoutingKey(InMemoryRoutingAttribute attribute, Type type);
    IInMemoryRoutingBuilder SetExchangeType(InMemoryRoutingAttribute attribute);
    InMemoryRouting Build(Type type);
}