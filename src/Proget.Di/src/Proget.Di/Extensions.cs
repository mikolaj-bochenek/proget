namespace Proget.Di;

public static class Extensions
{
    public static void AddScopedServices<TInterface>(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScopedServices(typeof(TInterface), assemblies);
    }

    public static void AddScopedServices(this IServiceCollection services, Type @interface, params Assembly[] assemblies)
    {
        services.Scan(s => s
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(@interface)
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }
}
