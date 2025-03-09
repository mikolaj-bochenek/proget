namespace Proget.Web.Cqrs;

public static class PutExtensions
{
    public static IEndpointRouteBuilder MapPutCqrs<TCommand, TResponse, TId>(
        this IEndpointRouteBuilder builder,
        string route,
        Func<TCommand, TId, TCommand> preprocess,
        Func<TCommand, TResponse> postprocess,
        Action<RouteHandlerBuilder> configureEndpoint)
        where TCommand : class, ICommand
        where TResponse : class
        where TId : notnull
    {
        var routeHandlerBuilder = builder.MapPut(route, async ([FromRoute] TId id, [FromBody] TCommand command, ICommandDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            command = preprocess(command, id);
            
            await dispatcher.SendAsync(command, cancellationToken);
            
            var response = postprocess(command);
            return Results.Ok(response);
        });

        configureEndpoint(routeHandlerBuilder);

        return builder;
    }
}