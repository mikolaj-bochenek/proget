namespace Proget.Cqrs.Events;

public static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddScopedServices(typeof(IEventHandler<>), assemblies);
        return services;
    }
}