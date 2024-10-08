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
        => ToCaseTables(modelBuilder, x => x.Kebaberize());

    public static void ToSnakeCaseTables(this ModelBuilder modelBuilder)
        => ToCaseTables(modelBuilder, x => x.Underscore());


    private static void ToCaseTables(ModelBuilder modelBuilder, Func<string, string> convertCase)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName() ?? string.Empty;
            var convertedTableName = convertCase(tableName);
            entity.SetTableName(convertedTableName);

            var tableObjectIdentifier = StoreObjectIdentifier.Table(convertedTableName, entity.GetSchema());

            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName(tableObjectIdentifier) ?? string.Empty;
                var convertedColumnName = convertCase(columnName);
                property.SetColumnName(convertedColumnName);
            }

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName() ?? string.Empty;
                var convertedKeyName = convertCase(keyName);
                key.SetName(convertedKeyName);
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var constraintName = foreignKey.GetConstraintName() ?? string.Empty;
                var convertedConstraintName = convertCase(constraintName);
                foreignKey.SetConstraintName(convertedConstraintName);
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName() ?? string.Empty;
                var convertedIndexName = convertCase(indexName);
                index.SetDatabaseName(convertedIndexName);
            }
        }
    }
}