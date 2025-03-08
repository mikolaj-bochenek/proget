namespace Proget.Web;

public static class CustomerExtensions
{
    public static IServiceCollection AddCustomerAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped(typeof(ICustomerAccessor<>), typeof(CustomerAccessor<>));

        return services;
    }
}
