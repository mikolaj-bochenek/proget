namespace Proget.Persistence.Mongo;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services,
        string? section = "mongo")
    {
        services.AddValidateOptions<MongoOptions>(section);

        var options = services.GetOptions<MongoOptions>(section);
        var client = new MongoClient(options.ConnectionString);
        services.AddSingleton(_ => client.GetDatabase(options.Database));

        ConventionRegistry.Register("default", new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
        }, _ => true);

        services.AddSingleton(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));

        return services;
    }
}
