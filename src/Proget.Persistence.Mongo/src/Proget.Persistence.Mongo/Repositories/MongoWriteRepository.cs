namespace Proget.Persistence.Mongo.Repositories;

internal sealed class MongoWriteRepository<T> : IMongoWriteRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoWriteRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<T>(typeof(T).Name.Camelize().Pluralize());
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _collection.InsertOneAsync(entity, null, cancellationToken);
}
