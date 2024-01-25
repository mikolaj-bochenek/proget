namespace Proget.Modularity;

public interface IModule
{
    string Name { get; }
    string Path { get; }
    void Register(IServiceCollection services);
    void Use(IApplicationBuilder app);
    void Expose(IEndpointRouteBuilder endpoints);
}