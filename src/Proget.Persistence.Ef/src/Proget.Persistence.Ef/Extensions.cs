namespace Proget.Persistence.Ef;

public static class Extensions
{
    public static IServiceCollection AddEfCore(
        this IServiceCollection services,
        Func<IEfOptionsConfigurator, IEfOptionsConfigurator> builder)
    {
        builder(new EfOptionsConfigurator(services));
        return services;
    }

    public static void ToKebabCaseTables(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var name = entity.GetTableName() ?? string.Empty;
            var kebabCaseName = name.Kebaberize().ToLower();
            entity.SetTableName(kebabCaseName);

            var tableObjectIdentifier = StoreObjectIdentifier.Table(kebabCaseName, entity.GetSchema());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName(tableObjectIdentifier)?.Kebaberize().ToLower());
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()?.Kebaberize().ToLower());
            }

            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName()?.Kebaberize().ToLower());
            }
        }
    }
}
