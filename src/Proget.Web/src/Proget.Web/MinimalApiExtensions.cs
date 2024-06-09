namespace Proget.Web;

public static class MinimalApiExtensions
{    
    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services)
        => services.AddMinimalEndpoints(Assembly.GetCallingAssembly());

    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScopedServices<IMinimalEndpoint>(assemblies);
        return services;
    }

    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder router)
        => router.MapMinimalEndpoints(Assembly.GetCallingAssembly());

    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder builder, params Assembly[] assemblies)
    {
        using var scope = builder.ServiceProvider.CreateScope();
        var endpoints = scope.ServiceProvider.GetServices<IMinimalEndpoint>();

        if (assemblies.Length > 0)
        {
            endpoints = endpoints.Where(e => assemblies.Contains(e.GetType().Assembly));
        }

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return builder;
    }
}
