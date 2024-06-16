namespace Proget.Persistence.Ef.Builders;

internal sealed class EfOptionsBuilder(IServiceCollection services) : IEfOptionsBuilder
{
    public IServiceCollection Services => services;

    public IEfOptionsBuilder AddReadRepository<T>()
    {
        EnsureIsAssignableToGenericInterface<T>(typeof(IEfReadRepository<,>));
        Services.AddScoped(typeof(IEfReadRepository<,>), typeof(T));
        return this;
    }

    public IEfOptionsBuilder AddReadRepository()
    {
        Services.AddScoped(typeof(IEfReadRepository<,>), typeof(EfReadRepository<,>));
        return this;
    }

    public IEfOptionsBuilder AddWriteRepository<T>()
    {
        EnsureIsAssignableToGenericInterface<T>(typeof(IEfWriteRepository<,>));
        Services.AddScoped(typeof(IEfWriteRepository<,>), typeof(T));
        return this;
    }

    public IEfOptionsBuilder AddWriteRepository()
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