namespace Proget.Messaging.Cqrs;

public static class Extensions
{
    public static IMessageSubscriber SubscribeEvent<TEvent>(this IMessageSubscriber subscriber)
        where TEvent : class, IMessage, IEvent
    {
        return subscriber.Subscribe<TEvent>(async (serviceProvider, @event) => {
            using var scope = serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();
            await handler.HandleAsync(@event);
        });
    }

    public static IMessageSubscriber SubscribeCommand<TCommand>(this IMessageSubscriber subscriber)
        where TCommand : class, IMessage, ICommand
    {
        return subscriber.Subscribe<TCommand>(async (serviceProvider, command) => {
            using var scope = serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        });
    }
}