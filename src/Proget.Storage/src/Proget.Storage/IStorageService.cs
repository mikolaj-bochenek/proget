namespace Proget.Storage;

public interface IStorageService
{
    Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken = default);
}