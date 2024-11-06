namespace Proget.Storage.CloudStorage.Services;

internal sealed class CloudStorageService : IStorageServiceStrategy
{
    private readonly StorageClient _storageClient;
    private readonly CloudStorageOptions _options;

    public CloudStorageService(IOptions<CloudStorageOptions> options)
    {
        _options = options.Value;

        var credential = GoogleCredential.FromFile(_options.KeyPath);
        _storageClient = StorageClient.Create(credential);
    }
    
    public async Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken = default)
    {
        using var fileStream = formFile.OpenReadStream();

        await _storageClient.UploadObjectAsync(_options.Bucket, formFile.FileName, formFile.ContentType, fileStream);
    }

    public async Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();

        await _storageClient.DownloadObjectAsync(_options.Bucket, fileName, memoryStream, null, cancellationToken);

        memoryStream.Position = 0;
        
        return memoryStream;
    }
}