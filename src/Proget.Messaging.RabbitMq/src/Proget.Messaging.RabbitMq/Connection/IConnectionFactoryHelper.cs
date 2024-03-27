namespace Proget.Messaging.RabbitMq.Connection;

internal interface IConnectionFactoryHelper
{
    IConnection Create();
}