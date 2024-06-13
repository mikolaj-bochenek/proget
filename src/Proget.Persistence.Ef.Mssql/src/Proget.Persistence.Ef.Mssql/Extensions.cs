namespace Proget.Persistence.Ef.Mssql;

public static class Extensions
{
    public static IPersistenceOptionsBuilder AddMssql<TContext>(
        this IPersistenceOptionsBuilder persistenceOptionsBuilder,
        string? section = "persistence:mssql")
        where TContext : DbContext, IDbContext
    {
        var services = persistenceOptionsBuilder.Services;
        var options = services.GetOptions<MssqlOptions>(section);
        services.AddValidateOptions<MssqlOptions>(section);

        services.AddDbContext<TContext>(
            ctx => ctx.UseSqlServer(options.ConnectionString));

        return persistenceOptionsBuilder;
    }
}