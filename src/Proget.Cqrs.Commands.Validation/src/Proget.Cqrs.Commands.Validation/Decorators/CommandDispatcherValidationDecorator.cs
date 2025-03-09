namespace Proget.Cqrs.Commands.Validation.Decorators;

internal sealed class CommandDispatcherValidationDecorator : ICommandDispatcher
{
    private readonly ICommandDispatcher _next;
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcherValidationDecorator(ICommandDispatcher commandDispatcher, IServiceProvider serviceProvider)
    {
        _next = commandDispatcher;
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetService<IValidator<TCommand>>();

        if (validator is not null)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        await _next.SendAsync(command, cancellationToken);
    }
}
