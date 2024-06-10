namespace Proget.Cqrs.Commands.Dispatchers;

internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<ICommandHandler<TCommand>>();
        var tasks = handlers.Select(x => x.HandleAsync(command, cancellationToken));
        await Task.WhenAll(tasks);
    }
}
