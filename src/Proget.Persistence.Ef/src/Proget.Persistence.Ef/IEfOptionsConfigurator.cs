namespace Proget.Persistence.Ef;

public interface IEfOptionsConfigurator
{
    public IServiceCollection Services { get; }
    IEfOptionsConfigurator AddReadRepository<T>();
    IEfOptionsConfigurator AddWriteRepository<T>();
    IEfOptionsConfigurator AddWriteRepository();
    IEfOptionsConfigurator AddReadRepository();
}