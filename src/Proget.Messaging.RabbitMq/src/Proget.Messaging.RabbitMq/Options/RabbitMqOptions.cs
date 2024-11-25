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

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }

    public RabbitMqChannelOptions? Channel { get; set; }

    public RabbitMqExchangeOptions? Exchange { get; set; }

    public RabbitMqQueueOptions? Queue { get; set; }
}

internal sealed class RabbitMqChannelOptions
{
    [Required(ErrorMessage = $"{nameof(BasicAck)} option is required.")]
    public bool BasicAck { get; set; }

    [Required(ErrorMessage = $"{nameof(ComplexAck)} option is required.")]
    public bool ComplexAck { get; set; }

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }
}

internal sealed class RabbitMqExchangeOptions
{
    [Required(ErrorMessage = $"{nameof(Name)} option is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Type)} option is required.")]
    public RabbitMqExchangeType Type { get; set; }

    [Required(ErrorMessage = $"{nameof(Declare)} option is required.")]
    public bool Declare { get; set; }

    [Required(ErrorMessage = $"{nameof(Durable)} option is required.")]
    public bool Durable { get; set; }

    [Required(ErrorMessage = $"{nameof(AutoDelete)} option is required.")]
    public bool AutoDelete { get; set; }

    [Required(ErrorMessage = $"{nameof(Arguments)} option is required.")]
    public Dictionary<string, object>? Arguments { get; set; }

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }
}

internal sealed class RabbitMqQueueOptions
{
    [Required(ErrorMessage = $"{nameof(Name)} option is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Declare)} option is required.")]
    public bool Declare { get; set; }

    [Required(ErrorMessage = $"{nameof(Durable)} option is required.")]
    public bool Durable { get; set; }

    [Required(ErrorMessage = $"{nameof(Exclusive)} option is required.")]
    public bool Exclusive { get; set; }

    [Required(ErrorMessage = $"{nameof(AutoDelete)} option is required.")]
    public bool AutoDelete { get; set; }

    [Required(ErrorMessage = $"{nameof(PrefetchSize)} option is required.")]
    public uint PrefetchSize { get; set; }

    [Required(ErrorMessage = $"{nameof(PrefetchCount)} option is required.")]
    public ushort PrefetchCount { get; set; }

    [Required(ErrorMessage = $"{nameof(Global)} option is required.")]
    public bool Global { get; set; }

    [Required(ErrorMessage = $"{nameof(AutoAck)} option is required.")]
    public bool AutoAck { get; set; }

    [Required(ErrorMessage = $"{nameof(MultipleAck)} option is required.")]
    public bool MultipleAck { get; set; }

    [Required(ErrorMessage = $"{nameof(MultipleNack)} option is required.")]
    public bool MultipleNack { get; set; }

    [Required(ErrorMessage = $"{nameof(RequeueRejected)} option is required.")]
    public bool RequeueRejected { get; set; }

    [Required(ErrorMessage = $"{nameof(Logger)} option is required.")]
    public bool Logger { get; set; }

    [Required(ErrorMessage = $"{nameof(AutoBind)} option is required.")]
    public bool AutoBind { get; set; }

    [Required(ErrorMessage = $"{nameof(BindExchange)} option is required.")]
    public string BindExchange { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(BindRoutingKey)} option is required.")]
    public string BindRoutingKey { get; set; } = string.Empty;
}
