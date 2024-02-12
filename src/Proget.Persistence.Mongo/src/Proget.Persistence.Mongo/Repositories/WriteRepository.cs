namespace Proget.Persistence.Mongo.Repositories;

internal sealed class WriteRepository<T> : IWriteRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public WriteRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<T>(typeof(T).Name.Camelize().Pluralize());
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _collection.InsertOneAsync(entity, null, cancellationToken);
}
