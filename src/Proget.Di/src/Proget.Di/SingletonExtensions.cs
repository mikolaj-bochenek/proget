namespace Proget.Di;

public static class SingletonExtensions
{
    public static void AddSingletonServices<TInterface>(this IServiceCollection services, Assembly assembly)
        where TInterface : class
        => AddSingletonServices(services, typeof(TInterface), [assembly]);

    public static void AddSingletonServices<TInterface>(this IServiceCollection services, Assembly[] assemblies)
        where TInterface : class
        => AddSingletonServices(services, typeof(TInterface), assemblies);

    public static void AddSingletonServices(this IServiceCollection services, Type interfaceType, Assembly assembly)
        => AddSingletonServices(services, interfaceType, [assembly]);
    
    public static void AddSingletonServices(this IServiceCollection services, Type interfaceType, Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(interfaceType)
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }
}
