namespace Proget.Persistence.Ef.Postgres;

public static class Extensions
{
    public static IEfOptionsBuilder AddPostgres<TContext>(
        this IEfOptionsBuilder efOptionsBuilder,
        string? section = "ef:postgres")
        where TContext : DbContext, IDbContext
    {
        var services = efOptionsBuilder.Services;
        var options = services.GetOptions<PostgresOptions>(section);
        
        services.AddValidateOptions<PostgresOptions>(section);
        services.AddDbContext<TContext>(
            ctx => ctx.UseNpgsql(options.ConnectionString));
        
        return efOptionsBuilder;
    }
}
