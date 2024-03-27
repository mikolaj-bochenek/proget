namespace Proget.Messaging.RabbitMq;

public static class Extensions
{
    public static IMessagingOptionsBuilder AddRabbitMq(this IMessagingOptionsBuilder messagingOptionsBuilder,
        string? section = "messaging:rabbitmq")
    {
        var services = messagingOptionsBuilder.Services;
        services.AddValidateOptions<RabbitMqOptions>(section);

        services.AddSingleton<IRabbitMqRoutingBuilder, RabbitMqRoutingBuilder>();
        services.AddSingleton<IRabbitMqRoutingFactory, RabbitMqRoutingFactory>();

        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IConnectionFactoryHelper, ConnectionFactoryHelper>();

        var connectionFactory = services.BuildServiceProvider().GetRequiredService<IConnectionFactoryHelper>();
        var connection = connectionFactory.Create();
        services.AddSingleton(connection);
        
        services.AddSingleton<ISubscriptionConsumer, RabbitMqSubscriptionConsumer>();

        return messagingOptionsBuilder;
    }
}
