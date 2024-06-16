namespace Proget.Persistence.Mongo;

public interface IMongoWriteRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
}