namespace Proget.Persistence.Ef.Postgres;

public static class Extensions
{
    public static IPersistenceOptionsBuilder AddPostgres<TContext>(
        this IPersistenceOptionsBuilder persistenceOptionsBuilder,
        string? section = "persistence:postgres")
        where TContext : DbContext, IDbContext
    {
        var services = persistenceOptionsBuilder.Services;
        var options = services.GetOptions<PostgresOptions>(section);
        services.AddValidateOptions<PostgresOptions>(section);

        services.AddDbContext<TContext>(
            ctx => ctx.UseNpgsql(options.ConnectionString));

        return persistenceOptionsBuilder;
    }
}