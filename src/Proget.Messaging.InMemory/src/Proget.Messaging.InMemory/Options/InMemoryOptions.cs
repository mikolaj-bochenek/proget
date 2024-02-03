namespace Proget.Messaging.InMemory.Options;

internal sealed class InMemoryOptions
{
    public ExchangeTypes? Exchange { get; set; }
    public string? RoutingKey { get; set; }
}