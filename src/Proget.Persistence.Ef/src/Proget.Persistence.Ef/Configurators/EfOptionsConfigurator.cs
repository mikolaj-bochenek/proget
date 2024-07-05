namespace Proget.Persistence.Ef.Configurators;

internal sealed class EfOptionsConfigurator(IServiceCollection services) : IEfOptionsConfigurator
{
    public IServiceCollection Services => services;

    public IEfOptionsConfigurator AddReadRepository<T>()
    {
        EnsureIsAssignableToGenericInterface<T>(typeof(IEfReadRepository<,>));
        Services.AddScoped(typeof(IEfReadRepository<,>), typeof(T));
        return this;
    }

    public IEfOptionsConfigurator AddReadRepository()
    {
        Services.AddScoped(typeof(IEfReadRepository<,>), typeof(EfReadRepository<,>));
        return this;
    }

    public IEfOptionsConfigurator AddWriteRepository<T>()
    {
        EnsureIsAssignableToGenericInterface<T>(typeof(IEfWriteRepository<,>));
        Services.AddScoped(typeof(IEfWriteRepository<,>), typeof(T));
        return this;
    }

    public IEfOptionsConfigurator AddWriteRepository()
    {
        Services.AddScoped(typeof(IEfWriteRepository<,>), typeof(EfWriteRepository<,>));
        return this;
    }

    private static void EnsureIsAssignableToGenericInterface<T>(Type genericInterfaceType)
    {
        var interfaceType = typeof(T).GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType)
            ?? throw new ArgumentException($"{typeof(T).Name} does not implement {genericInterfaceType.Name}");
    }

}