namespace Proget.Storage.Services;

internal sealed class StorageService : IStorageService
{
    private readonly IEnumerable<IStorageServiceStrategy> _strategies;

    public StorageService(IEnumerable<IStorageServiceStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken = default)
    {
        foreach (var strategy in _strategies)
        {
            await strategy.UploadAsync(formFile, cancellationToken);
        }
    }
}
