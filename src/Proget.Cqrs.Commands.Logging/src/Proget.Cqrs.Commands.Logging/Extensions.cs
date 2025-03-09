namespace Proget.Cqrs.Commands.Logging;

public static class Extensions
{
    public static ICommandBuilder AddLogging(this ICommandBuilder builder)
    {
        builder.Services.Decorate<ICommandDispatcher, CommandDispatcherLoggingDecorator>();
        return builder;
    }
}
