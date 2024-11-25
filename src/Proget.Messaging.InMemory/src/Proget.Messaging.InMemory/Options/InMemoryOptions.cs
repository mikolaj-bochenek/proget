namespace Proget.Messaging.InMemory.Options;

internal sealed class InMemoryOptions
{
    [Required(ErrorMessage = $"{nameof(ExchangeType)} option is required.")]
    public InMemoryExchangeType ExchangeType { get; set; }

    [Required(ErrorMessage = $"{nameof(RoutingKey)} option is required.")]
    public string RoutingKey { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }
}
