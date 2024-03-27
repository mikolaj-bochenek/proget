namespace Proget.Messaging.RabbitMq.Routing;

internal sealed record RabbitMqRouting(
    Type Type,
    ExchangeTypes ExchangeType,
    string Exchange,
    string RoutingKey,
    string Queue
);