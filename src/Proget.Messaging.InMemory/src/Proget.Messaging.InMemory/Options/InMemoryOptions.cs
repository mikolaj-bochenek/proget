namespace Proget.Messaging.InMemory.Options;

internal sealed class InMemoryOptions
{
    public InMemoryExchangeType ExchangeType { get; set; }

    public string RoutingKey { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }
}
