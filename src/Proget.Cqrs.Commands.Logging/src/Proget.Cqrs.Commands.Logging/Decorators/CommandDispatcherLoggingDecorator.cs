namespace Proget.Cqrs.Commands.Logging.Decorators;

internal sealed class CommandDispatcherLoggingDecorator : ICommandDispatcher
{
    private readonly ICommandDispatcher _next;
    private readonly ILogger<CommandDispatcherLoggingDecorator> _logger;

    public CommandDispatcherLoggingDecorator(ICommandDispatcher commandDispatcher, ILogger<CommandDispatcherLoggingDecorator> logger)
    {
        _next = commandDispatcher;
        _logger = logger;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        _logger.LogInformation("Processing command {CommandType}", typeof(TCommand).Name);

        try
        {
            await _next.SendAsync(command, cancellationToken);
            _logger.LogInformation("Command {CommandType} processed successfully", typeof(TCommand).Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing command {CommandType}", typeof(TCommand).Name);
            throw;
        }
    }
}
