namespace Proget.Persistence.Ef.Mssql;

public static class Extensions
{
    public static IEfOptionsBuilder AddMssql<TContext>(
        this IEfOptionsBuilder efOptionsBuilder,
        string? section = "ef:postgres")
        where TContext : DbContext, IDbContext
    {
        var services = efOptionsBuilder.Services;
        var options = services.GetOptions<MssqlOptions>(section);

        services.AddValidateOptions<MssqlOptions>(section);
        services.AddDbContext<TContext>(
            ctx => ctx.UseSqlServer(options.ConnectionString));
        
        services.AddScoped<IDbContext, TContext>();

        return efOptionsBuilder;
    }
}
