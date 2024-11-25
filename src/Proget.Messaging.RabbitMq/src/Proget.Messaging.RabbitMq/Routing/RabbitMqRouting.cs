namespace Proget.Messaging.RabbitMq.Routing;

internal sealed record RabbitMqRouting(
    Type Type,
    string Exchange,
    string RoutingKey,
    string Queue
);