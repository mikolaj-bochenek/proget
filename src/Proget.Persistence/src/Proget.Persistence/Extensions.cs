namespace Proget.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        Func<IPersistenceOptionsBuilder, IPersistenceOptionsBuilder> optionsBuilder
    )
    {
        optionsBuilder(new PersistenceOptionsBuilder(services));
        return services;
    }
}
