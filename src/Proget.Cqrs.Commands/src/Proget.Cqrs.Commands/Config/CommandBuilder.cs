
namespace Proget.Cqrs.Commands.Config;

internal sealed class CommandBuilder : ICommandBuilder
{
    public IServiceCollection Services { get; }
    public Assembly[] Assemblies { get; }

    public CommandBuilder(IServiceCollection services, Assembly[] assemblies)
    {
        Services = services;
        Assemblies = assemblies;
    }
}