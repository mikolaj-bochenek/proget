namespace Proget.Web.Exceptions;

public static class Extenions
{
    private const string ErrorHandlerMiddlewareAddedKey = "ErrorHandlerMiddlewareAdded";

    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<DefaultExceptionMapper>();

    public static IServiceCollection AddExceptionHandler<TExceptionMapper>(this IServiceCollection services)
        where TExceptionMapper : class, IExceptionMapper
    {
        services.AddTransient<IExceptionMapper, TExceptionMapper>();
        services.AddTransient<ErrorHandlerMiddleware>();
       
        return services;
    }

    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        if (!builder.Properties.ContainsKey(ErrorHandlerMiddlewareAddedKey))
        {
            builder.Properties[ErrorHandlerMiddlewareAddedKey] = true;
            builder.UseMiddleware<ErrorHandlerMiddleware>();
        }

        return builder;
    }
}
