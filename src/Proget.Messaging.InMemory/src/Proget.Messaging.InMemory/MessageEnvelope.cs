namespace Proget.Messaging.InMemory;

internal sealed record MessageEnvelope(
    InMemoryExchangeType ExchangeType,
    string RoutingKey,
    byte[] Body
);