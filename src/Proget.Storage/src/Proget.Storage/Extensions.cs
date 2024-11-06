namespace Proget.Storage;

public static class Extensions
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        Func<IStorageOptionsBuilder, IStorageOptionsBuilder> optionsBuilder
    )
    {
        optionsBuilder(new StorageOptionsBuilder(services));

        services.AddSingleton<IStorageService, StorageService>();

        return services;
    }
}
