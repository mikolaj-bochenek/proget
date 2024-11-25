namespace Proget.Messaging.RabbitMq.Routing;

internal interface IRabbitMqRoutingBuilder
{
    IRabbitMqRoutingBuilder SetExchange(Type type);
    IRabbitMqRoutingBuilder SetRoutingKey(Type type);
    IRabbitMqRoutingBuilder SetQueue(Type type);
    RabbitMqRouting Build(Type type);
}