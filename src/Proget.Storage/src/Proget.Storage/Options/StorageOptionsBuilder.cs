namespace Proget.Storage.Options;

internal sealed class StorageOptionsBuilder : IStorageOptionsBuilder
{
    public IServiceCollection Services { get; }

    public StorageOptionsBuilder(IServiceCollection services)
    {
        Services = services;
    }
    
}