namespace Proget.Web;

public sealed class WebOptions
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Version is required")]
    public string Version { get; set; } = string.Empty;

    public bool DisplayBanner { get; set; }
}