namespace Proget.Persistence.Ef;

internal sealed class EfWriteRepository<TEntity, TContext> : IEfWriteRepository<TEntity, TContext>
    where TEntity : class
    where TContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly TContext _context;

    public EfWriteRepository(TContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }

    public async Task AddAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        if (saveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        if (saveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        if (saveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        if (saveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        if (saveChanges)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}