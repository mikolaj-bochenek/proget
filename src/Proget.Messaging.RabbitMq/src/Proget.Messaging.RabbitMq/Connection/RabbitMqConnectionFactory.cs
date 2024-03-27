namespace Proget.Messaging.RabbitMq.Connection;

internal sealed class ConnectionFactoryHelper : IConnectionFactoryHelper
{
    private readonly RabbitMqOptions _options;

    public ConnectionFactoryHelper(RabbitMqOptions options)
    {
        _options = options;
    }

    public IConnection Create()
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            VirtualHost = _options.VirtualHost,
            Port = _options.Port,
            UserName = _options.Username,
            Password = _options.Password
        };
        return factory.CreateConnection();
    }
}