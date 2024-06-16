namespace Proget.Persistence.Ef;

public interface IEfReadRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
}