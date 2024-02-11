namespace Proget.Persistence.Options;

internal sealed class PersistenceOptionsBuilder : IPersistenceOptionsBuilder
{
    public IServiceCollection Services { get; }

    public PersistenceOptionsBuilder(IServiceCollection services)
    {
        Services = services;
    }
}