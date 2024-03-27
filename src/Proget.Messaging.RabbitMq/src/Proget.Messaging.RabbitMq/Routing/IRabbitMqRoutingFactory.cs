namespace Proget.Messaging.RabbitMq.Routing;

internal interface IRabbitMqRoutingFactory
{
    RabbitMqRouting Get<TMessage>() where TMessage : class, IMessage;
    RabbitMqRouting Get(Type type);
}
