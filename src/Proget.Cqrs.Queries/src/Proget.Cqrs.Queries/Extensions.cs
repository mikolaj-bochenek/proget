namespace Proget.Cqrs.Queries;

public static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services)
        => services.AddQueries(Assembly.GetCallingAssembly());

    public static IServiceCollection AddQueries(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.AddScopedServices(typeof(IQueryHandler<,>), assemblies);
        return services;
    }
}
