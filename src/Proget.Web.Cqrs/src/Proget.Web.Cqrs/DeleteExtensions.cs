namespace Proget.Web.Cqrs;

public static class DeleteExtensions
{
    public static IEndpointRouteBuilder MapDeleteCqrs<TCommand, TResponse, TId>(
        this IEndpointRouteBuilder builder,
        string route,
        Func<TId, TCommand> preprocess,
        Func<TCommand, TResponse> postprocess,
        Action<RouteHandlerBuilder> configureEndpoint)
        where TCommand : class, ICommand
        where TResponse : class
        where TId : notnull
    {
        var routeHandlerBuilder = builder.MapDelete(route, async ([FromRoute] TId id, ICommandDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var command = preprocess(id);
            
            await dispatcher.SendAsync(command, cancellationToken);
            
            var response = postprocess(command);
            return Results.Ok(response);
        });

        configureEndpoint(routeHandlerBuilder);

        return builder;
    }
}