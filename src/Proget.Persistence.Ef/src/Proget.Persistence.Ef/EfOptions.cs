namespace Proget.Persistence.Ef;

public abstract class EfOptions
{
    [Required(ErrorMessage = $"{nameof(ConnectionString)} option is required.")]
    public string ConnectionString { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MigrationsEnabled)} option is required.")]
    public bool MigrationsEnabled { get; set; }

    [Required(ErrorMessage = $"{nameof(MigrationsHistorySchema)} option is required.")]
    public string MigrationsHistorySchema { get; set; } = string.Empty;
}
