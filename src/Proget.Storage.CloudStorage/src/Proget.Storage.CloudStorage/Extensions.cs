namespace Proget.Storage.CloudStorage;

public static class Extensions
{
    public static IStorageOptionsBuilder AddCloudStorage(this IStorageOptionsBuilder storageOptionsBuilder,
        string? section = "storage:cloudStorage")
    {
        var services = storageOptionsBuilder.Services;
        services.AddValidateOptions<CloudStorageOptions>(section);

        services.AddSingleton<IStorageServiceStrategy, CloudStorageService>();

        return storageOptionsBuilder;
    }
}
