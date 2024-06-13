namespace Proget.Persistence.Ef;

public abstract class EfOptions
{
    [Required(ErrorMessage = $"{nameof(ConnectionString)} option is required.")]
    public string ConnectionString { get; set; } = string.Empty;
}
