namespace Proget.Cqrs.Commands.Config;

internal sealed class CommandBuilder : ICommandBuilder
{
    public IServiceCollection Services { get; }

    public CommandBuilder(IServiceCollection services)
    {
        Services = services;
    }
}