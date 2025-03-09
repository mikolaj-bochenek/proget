namespace Proget.Cqrs.Commands;

public interface ICommandBuilder
{
    IServiceCollection Services { get; }
    Assembly[] Assemblies { get; }
}
