namespace Proget.Di;

public static class ScopedExtensions
{
    public static void AddScopedServices<TInterface>(this IServiceCollection services, Assembly assembly)
        where TInterface : class
        => AddScopedServices(services, typeof(TInterface), [assembly]);

    public static void AddScopedServices<TInterface>(this IServiceCollection services, Assembly[] assemblies)
        where TInterface : class
        => AddScopedServices(services, typeof(TInterface), assemblies);

    public static void AddScopedServices(this IServiceCollection services, Type interfaceType, Assembly assembly)
        => AddScopedServices(services, interfaceType, [assembly]);
    
    public static void AddScopedServices(this IServiceCollection services, Type interfaceType, Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(interfaceType)
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
