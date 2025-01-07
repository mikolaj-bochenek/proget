namespace Proget.Messaging;

public static class Extensions
{
    public static IMessageSubscriber UseMessaging(this IApplicationBuilder app)
        => new MessageSubscriber(app.ApplicationServices.GetRequiredService<ISubscriptionsRegistry>());

    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        Func<IMessagingOptionsBuilder, IMessagingOptionsBuilder> optionsBuilder
    )
    {
        optionsBuilder(new MessagingOptionsBuilder(services));

        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();

        services.AddSingleton<ISubscriptionsRegistry, ChannelSubscriptionsRegistry>();
        services.AddHostedService<SubscriptionConsumerJob>();

        services.AddSingleton<ISerializer, NewtonsoftJsonSerializer>();

        return services;
    }
}
