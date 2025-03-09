namespace Proget.Cqrs.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<ICommandBuilder>? configure = null)
        => services.AddCommands(configure, Assembly.GetCallingAssembly());

    public static IServiceCollection AddCommands(this IServiceCollection services, Action<ICommandBuilder>? configure = null, params Assembly[] assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddScopedServices(typeof(ICommandHandler<>), assemblies);

        var builder = new CommandBuilder(services);
        configure?.Invoke(builder);
        
        return services;
    }
}
