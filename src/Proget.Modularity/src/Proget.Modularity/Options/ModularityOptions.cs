namespace Proget.Modularity.Options;

internal sealed class ModularityOptions
{
    [Required(ErrorMessage = "ModulePrefix is required")]
    public string ModulePrefix { get; set; } = string.Empty;

    public bool FrameworkEnabled { get; set; }

    public bool LoggingEnabled { get; set; }

    public bool Enabled { get; set; } = true;
}