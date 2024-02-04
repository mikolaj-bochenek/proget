namespace Proget.Di;

public static class TransientExtensions
{
    public static void AddTransientServices<TInterface>(this IServiceCollection services, Assembly assembly)
        where TInterface : class
        => AddTransientServices(services, typeof(TInterface), [assembly]);

    public static void AddTransientServices<TInterface>(this IServiceCollection services, Assembly[] assemblies)
        where TInterface : class
        => AddTransientServices(services, typeof(TInterface), assemblies);

    public static void AddTransientServices(this IServiceCollection services, Type interfaceType, Assembly assembly)
        => AddTransientServices(services, interfaceType, [assembly]);
    
    public static void AddTransientServices(this IServiceCollection services, Type interfaceType, Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(interfaceType)
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}
