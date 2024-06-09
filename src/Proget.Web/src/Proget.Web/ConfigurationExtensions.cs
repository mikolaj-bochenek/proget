namespace Proget.Web;

public static class ConfigurationExtensions
{
    public static void AddValidateOptions<TModel>(this IServiceCollection services, string? section = null)
        where TModel : class, new()
    {
        ProcessOptionsSection<TModel>(section, services.AddModel<TModel>);
    }

    public static TModel GetOptions<TModel>(this IServiceCollection services, string? section = null)
        where TModel : class, new()
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        return configuration.GetOptions<TModel>(section);
    }

    public static TModel GetOptions<TModel>(this WebApplication app, string? section = null)
        where TModel : class, new()
    {
        return app.Configuration.GetOptions<TModel>(section);  
    }

    public static TModel GetOptions<TModel>(this IApplicationBuilder app, string? section = null)
        where TModel : class, new()
    {
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<TModel>(section); 
    }

    public static TModel GetOptions<TModel>(this IConfiguration configuration, string? section = null)
        where TModel : class, new()
    {
        TModel model = new();
        ProcessOptionsSection<TModel>(section, optionsPrefix =>
        {
            model = configuration.GetModel<TModel>(optionsPrefix);
        });

        return model;
    }

    private static TModel GetModel<TModel>(this IConfiguration configuration, string section)
        where TModel : class, new()
    {
        var model = new TModel();
        configuration.GetSection(section).Bind(model);
        return model;
    }

    private static void AddModel<TModel>(this IServiceCollection services, string section)
        where TModel : class, new()
    {
        services.AddOptions<TModel>()
            .BindConfiguration(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(x => x.GetRequiredService<IOptions<TModel>>().Value);
    }

    private static void ProcessOptionsSection<TModel>(string? section, Action<string> onSectionProcessed)
        where TModel : class, new()
    {
        if (!string.IsNullOrWhiteSpace(section))
        {
            onSectionProcessed(section.ToLowerInvariant());
            return;
        }

        var defaultName = typeof(TModel).Name;
        var optionsSuffix = "Options";

        if (!defaultName.EndsWith(optionsSuffix))
        {
            throw new InvalidOperationException(
                $"Either a section name must be provided, or the class '{defaultName}' must contain the suffix '{optionsSuffix}'."
            );
        }

        var optionsPrefix = defaultName[0..^optionsSuffix.Length];
        onSectionProcessed(optionsPrefix.ToLowerInvariant());
    }
}
