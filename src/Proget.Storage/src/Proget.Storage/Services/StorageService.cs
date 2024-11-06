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

    public async Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken = default)
    {
        foreach (var strategy in _strategies)
        {
            try
            {
                var stream = await strategy.DownloadAsync(fileName, cancellationToken);
                if (stream is not null)
                {
                    return stream;
                }
            }
            catch (Exception)
            {
                continue;
            }
        }

        throw new FileNotFoundException($"File '{fileName}' not found in any storage strategy.");
    }
}
