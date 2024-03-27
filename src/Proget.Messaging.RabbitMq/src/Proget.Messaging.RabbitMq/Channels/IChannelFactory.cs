namespace Proget.Messaging.RabbitMq.Channels;

public interface IChannelFactory
{
    IModel Create();
}
