namespace Proget.Modularity;

public static class Extensions
{
    public static IApplicationBuilder UseModularity(this IApplicationBuilder app)
    {
        var modules = app.ApplicationServices.GetRequiredService<List<IModule>>();
       
        modules.ForEach(m => m.Use(app));
        return app;
    }

    public static WebApplicationBuilder AddModularity(this WebApplicationBuilder builder, string? section = "modularity")
    {
        builder.Services.AddValidateOptions<ModularityOptions>(section);
        var options = builder.Services.GetOptions<ModularityOptions>(section);

        if (!options.Enabled)
        {
            return builder;
        }
        
        var assemblies = builder.LoadAssemblies(options.ModulePrefix);
        var modules = builder.LoadModules(assemblies);

        builder.Services.AddSingleton(assemblies);
        builder.Services.AddSingleton(modules);

        if (options.LoggingEnabled)
        {
            WriteLine("Enabled modules: {0}", string.Join(", ", modules.Select(m => m.Name)));
        }

        if (options.FrameworkEnabled)
        {
            builder.Services
                .AddEvents(assemblies)
                .AddMessaging(opt => opt
                    .AddInMemory()
                    .AddRabbitMq()
                );
        }
        
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
            .ToList();
        
        return modules;
    }

    private static IHostBuilder ConfigureModules(this IHostBuilder builder)
    {
        return builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var settings in GetSettings("*", 2))
            {
                cfg.AddJsonFile(settings, true, true);
            }

            foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}", 3))
            {
                cfg.AddJsonFile(settings, true, true);
            }

            cfg.AddEnvironmentVariables();

            IEnumerable<string> GetSettings(string pattern, int segments)
            {
                var files = Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
                
                return files.Where(file =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    var segmentsCount = fileName.Count(c => c == '.') + 1;
                    return segmentsCount == segments;
                });
            }
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