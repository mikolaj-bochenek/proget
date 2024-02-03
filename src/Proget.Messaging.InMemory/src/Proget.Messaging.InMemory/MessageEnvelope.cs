namespace Proget.Messaging.InMemory;

internal sealed record MessageEnvelope(
    ExchangeTypes Exchange,
    string RoutingKey,
    byte[] Body
);