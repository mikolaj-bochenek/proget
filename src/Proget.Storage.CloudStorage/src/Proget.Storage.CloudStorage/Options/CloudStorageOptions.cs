namespace Proget.Storage.CloudStorage.Options;

internal sealed class CloudStorageOptions
{
    [Required(ErrorMessage = $"{nameof(Bucket)} option is required.")]
    public string Bucket { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(KeyPath)} option is required.")]
    public string KeyPath { get; set; } = string.Empty;
}