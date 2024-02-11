namespace Proget.Persistence.Mongo;

public static class Extensions
{
    public static IPersistenceOptionsBuilder AddMongo(this IPersistenceOptionsBuilder persistenceOptionsBuilder,
        string? section = "persistence:mongo")
    {
        var services = persistenceOptionsBuilder.Services;
        services.AddValidateOptions<MongoOptions>(section);

        var options = services.GetOptions<MongoOptions>(section);
        var client = new MongoClient(options.ConnectionString);
        services.AddSingleton(_ => client.GetDatabase(options.Database));

        services.AddSingleton(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        return persistenceOptionsBuilder;
    }
}
