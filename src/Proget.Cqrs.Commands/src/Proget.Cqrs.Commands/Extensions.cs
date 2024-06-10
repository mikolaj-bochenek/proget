namespace Proget.Cqrs.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
        => services.AddCommands(Assembly.GetCallingAssembly());

    public static IServiceCollection AddCommands(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddScopedServices(typeof(ICommandHandler<>), assemblies);
        return services;
    }
}
