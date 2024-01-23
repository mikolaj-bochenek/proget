namespace Proget.Web;

public static class WebExtensions
{
    public static IServiceCollection AddWeb(this IServiceCollection services, string? section = "web")
    {
        services.AddValidateOptions<WebOptions>(section);
        var options = services.GetOptions<WebOptions>(section);

        if (!options.DisplayBanner)
        {
            return services;
        }

        WriteLine(FiggleFonts.Doom.Render($"{options.Name}:{options.Version}"));

        return services;
    }
}
