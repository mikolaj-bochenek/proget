namespace Proget.Web;

public static class WebExtensions
{
    public static WebApplicationBuilder AddWeb(this WebApplicationBuilder builder, string? section = "web")
    {
        builder.Services.AddValidateOptions<WebOptions>(section);
        var options = builder.Services.GetOptions<WebOptions>(section);

        if (!options.DisplayBanner)
        {
            return builder;
        }

        WriteLine(FiggleFonts.Doom.Render($"{options.Name}:{options.Version}"));

        return builder;
    }
}
