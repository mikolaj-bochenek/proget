namespace Proget.Persistence.Ef;

public interface IEfOptionsBuilder
{
    public IServiceCollection Services { get; }
    IEfOptionsBuilder AddReadRepository<T>();
    IEfOptionsBuilder AddWriteRepository<T>();
    IEfOptionsBuilder AddWriteRepository();
    IEfOptionsBuilder AddReadRepository();
}