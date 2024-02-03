namespace Proget.Messaging.InMemory;

public static class Extensions
{
    public static IMessagingOptionsBuilder AddInMemory(this IMessagingOptionsBuilder messagingOptionsBuilder,
        string? section = "messaging:inMemory")
    {
        var services = messagingOptionsBuilder.Services;
        services.AddValidateOptions<InMemoryOptions>();

        services.AddSingleton<IMessageRoutingBuilder, MessageRoutingBuilder>();
        services.AddSingleton<IMessageRoutingFactory, MessageRoutingFactory>();

        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageListener, MessageListener>();
        services.AddHostedService<MessageReaderJob>();

        services.AddSingleton<IMessagePublisherStrategy, InMemoryMessagePublisher>();
        services.AddSingleton<ISubscriptionConsumer, InMemorySubscriptionConsumer>();

        return messagingOptionsBuilder;
    }
}
