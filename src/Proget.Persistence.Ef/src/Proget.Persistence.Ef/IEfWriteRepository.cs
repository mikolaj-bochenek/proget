namespace Proget.Persistence.Ef;

public interface IEfWriteRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    Task AddAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);
}