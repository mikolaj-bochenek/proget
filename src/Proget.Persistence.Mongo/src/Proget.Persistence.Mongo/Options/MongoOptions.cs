namespace Proget.Persistence.Mongo.Options;

internal sealed class MongoOptions
{
    [Required(ErrorMessage = $"{nameof(ConnectionString)} option is required.")]
    public string ConnectionString { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Database)} option is required.")]
    public string Database { get; set; } = string.Empty;
}
