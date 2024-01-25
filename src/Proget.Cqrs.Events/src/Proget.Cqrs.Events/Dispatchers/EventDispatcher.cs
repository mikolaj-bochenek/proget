namespace Proget.Cqrs.Events;

internal sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
        await Task.WhenAll(tasks);
    }
}