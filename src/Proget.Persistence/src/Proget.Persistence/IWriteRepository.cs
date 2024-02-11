namespace Proget.Persistence;

public interface IWriteRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
}