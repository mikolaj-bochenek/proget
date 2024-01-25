namespace Proget.Modularity;

public static class Extensions
{
    public static WebApplicationBuilder AddModularity(this WebApplicationBuilder builder, string modulePrefix)
    {
        var assemblies = builder.LoadAssemblies(modulePrefix);
        var modules = builder.LoadModules(assemblies);
        
        builder.Services
            .AddEvents(assemblies);
        
        modules.ForEach(m => m.Register(builder.Services));

        return builder;
    }

    public static List<Assembly> LoadAssemblies(this WebApplicationBuilder builder, string modulePrefix)
    {
        builder.Host.ConfigureModules();
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        var disabledModules = new List<string>();
        foreach (var file in files)
        {
            if (!file.Contains(modulePrefix))
            {
                continue;
            }

            var moduleName = file.Split($"{modulePrefix}.").Last().Split(".").First().ToLowerInvariant();
            var enabled = builder.Configuration.GetValue<bool>($"{moduleName}:module:enabled");
            if (!enabled)
            {
                disabledModules.Add(file);
            }
        }

        foreach (var disabledModule in disabledModules)
        {
            files.Remove(disabledModule);
        }
            
        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        builder.Services.AddModuleControllers(disabledModules);

        return assemblies;
    }

    public static List<IModule> LoadModules(this WebApplicationBuilder _, IEnumerable<Assembly> assemblies)
    {
        var modules = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && x.IsClass)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .Tap(x => WriteLine($"Loaded module: {x.Name}"))
            .ToList();
        
        return modules;
    }

    private static IHostBuilder ConfigureModules(this IHostBuilder builder)
    {
        return builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var settings in GetSettings("*"))
            {
                cfg.AddJsonFile(settings);
            }

            foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
            {
                cfg.AddJsonFile(settings);
            }

            IEnumerable<string> GetSettings(string pattern)
                => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
        });
    }

    private static void AddModuleControllers(this IServiceCollection services, List<string> disabledModules)
    {
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            var appParts = disabledModules.SelectMany(x => manager.ApplicationParts
                .Where(part => part.Name.Contains(x, StringComparison.InvariantCultureIgnoreCase))).ToList();

            foreach (var part in appParts)
            {
                manager.ApplicationParts.Remove(part);
            }
                    
            manager.FeatureProviders.Add(new CustomControllerFeatureProvider());
        });
    }
}