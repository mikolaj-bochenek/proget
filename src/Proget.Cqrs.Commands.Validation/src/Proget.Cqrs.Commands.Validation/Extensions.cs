namespace Proget.Cqrs.Commands.Validation;

public static class Extensions
{
    public static ICommandBuilder AddValidation(this ICommandBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(builder.Assemblies);
        builder.Services.Decorate<ICommandDispatcher, CommandDispatcherValidationDecorator>();
        return builder;
    }
}
