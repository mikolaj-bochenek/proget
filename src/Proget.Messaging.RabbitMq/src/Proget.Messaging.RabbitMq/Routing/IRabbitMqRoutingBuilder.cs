namespace Proget.Messaging.RabbitMq.Routing;

internal interface IRabbitMqRoutingBuilder
{
    IRabbitMqRoutingBuilder SetExchange(RabbitMqRoutingAttribute attribute, Type type);
    IRabbitMqRoutingBuilder SetRoutingKey(RabbitMqRoutingAttribute attribute, Type type);
    IRabbitMqRoutingBuilder SetQueue(RabbitMqRoutingAttribute attribute, Type type);
    RabbitMqRouting Build(Type type);
}