namespace Proget.Messaging.InMemory.Routing;

internal sealed record MessageRouting(
    Type Type,
    ExchangeTypes Exchange,
    string RoutingKey
);