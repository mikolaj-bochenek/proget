namespace Proget.Persistence.Ef;

internal sealed class EfReadRepository<TEntity, TContext> : IEfReadRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;

    public EfReadRepository(TContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => await _dbSet.Where(expression).ToListAsync(cancellationToken);

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);
}