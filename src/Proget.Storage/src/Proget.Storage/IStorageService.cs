namespace Proget.Storage;

public interface IStorageService
{
    Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken = default);
    Task<Stream> DownloadAsync(string fileName, CancellationToken cancellationToken = default);
}