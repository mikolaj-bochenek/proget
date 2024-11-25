namespace Proget.Messaging.InMemory.Routing;

internal interface IInMemoryRoutingBuilder
{
    IInMemoryRoutingBuilder SetRoutingKey(Type type);
    IInMemoryRoutingBuilder SetExchangeType(Type type);
    InMemoryRouting Build(Type type);
}