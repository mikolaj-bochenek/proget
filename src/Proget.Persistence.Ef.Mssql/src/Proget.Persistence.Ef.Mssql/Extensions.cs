namespace Proget.Persistence.Ef.Mssql;

public static class Extensions
{
    public static IEfOptionsConfigurator AddMssql<TContext>(
        this IEfOptionsConfigurator efOptionsConfigurator,
        string? section = "ef:postgres")
        where TContext : DbContext, IDbContext
    {
        var services = efOptionsConfigurator.Services;
        var options = services.GetOptions<MssqlOptions>(section);

        services.AddValidateOptions<MssqlOptions>(section);
        services.AddDbContext<TContext>(
            ctx => ctx.UseSqlServer(options.ConnectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", options.MigrationsHistorySchema)));
        
        if (options.MigrationsEnabled)
        {
            var context = services.BuildServiceProvider().GetRequiredService<TContext>();
            context.Database.Migrate();
        }

        return efOptionsConfigurator;
    }
}
