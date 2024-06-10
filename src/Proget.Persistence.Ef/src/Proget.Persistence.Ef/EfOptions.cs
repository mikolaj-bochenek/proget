namespace Proget.Persistence.Ef;

public abstract class EfOptions
{
    [Required(ErrorMessage = $"{nameof(ConnectionString)} option is required.")]
    public string ConnectionString { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(MigrationsOutput)} option is required.")]
    public string MigrationsOutput { get; set; } = string.Empty;
}
