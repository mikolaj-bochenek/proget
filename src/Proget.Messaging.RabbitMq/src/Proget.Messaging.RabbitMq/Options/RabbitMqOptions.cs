namespace Proget.Messaging.RabbitMq.Options;

internal sealed class RabbitMqOptions
{
    [Required(ErrorMessage = $"{nameof(HostName)} option is required.")]
    public string HostName { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(VirtualHost)} option is required.")]
    public string VirtualHost { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Port)} option is required.")]
    public int Port { get; set; }

    [Required(ErrorMessage = $"{nameof(Username)} option is required.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Password)} option is required.")]
    public string Password { get; set; } = string.Empty;

    public ExchangeTypes? ExchangeType { get; set; }
    public string? Exchange { get; set; }
    public string? RoutingKey { get; set; }
    public string? Queue { get; set; }
}
