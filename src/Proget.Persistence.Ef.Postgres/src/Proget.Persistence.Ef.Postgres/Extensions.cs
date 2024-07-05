namespace Proget.Persistence.Ef.Postgres;

public static class Extensions
{
    public static IEfOptionsConfigurator AddPostgres<TContext>(
        this IEfOptionsConfigurator efOptionsConfigurator,
        string? section = "ef:postgres")
        where TContext : DbContext, IDbContext
    {
        var services = efOptionsConfigurator.Services;
        var options = services.GetOptions<PostgresOptions>(section);
        
        services.AddValidateOptions<PostgresOptions>(section);
        services.AddDbContext<TContext>(
            ctx => ctx.UseNpgsql(options.ConnectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", options.MigrationsHistorySchema)));
        
        if (options.MigrationsEnabled)
        {
            var context = services.BuildServiceProvider().GetRequiredService<TContext>();
            context.Database.Migrate();
        }
        
        return efOptionsConfigurator;
    }
}
