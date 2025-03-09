namespace Proget.Web.Cqrs;

public static class PostExtensions
{
    public static IEndpointRouteBuilder MapPostCqrs<TCommand, TResponse>(
        this IEndpointRouteBuilder builder,
        string route,
        Func<TCommand, TCommand> preprocess,
        Func<TCommand, TResponse> postprocess,
        Action<RouteHandlerBuilder> configureEndpoint)
        where TCommand : class, ICommand
        where TResponse : class
    {
        var routeHandlerBuilder = builder.MapPost(route, async (TCommand command, ICommandDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            command = preprocess(command);
            
            await dispatcher.SendAsync(command, cancellationToken);
            
            var response = postprocess(command);
            return Results.Ok(response);
        });

        configureEndpoint(routeHandlerBuilder);

        return builder;
    }
}