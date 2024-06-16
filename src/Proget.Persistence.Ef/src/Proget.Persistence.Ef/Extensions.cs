namespace Proget.Persistence.Ef;

public static class Extensions
{
    public static IServiceCollection AddEfCore(
        this IServiceCollection services,
        Func<IEfOptionsBuilder, IEfOptionsBuilder> builder)
    {
        builder(new EfOptionsBuilder(services));
        return services;
    }
}
