namespace Proget.Messaging.InMemory.Routing;

internal sealed record InMemoryRouting(
    Type Type,
    InMemoryExchangeType ExchangeType,
    string RoutingKey
);