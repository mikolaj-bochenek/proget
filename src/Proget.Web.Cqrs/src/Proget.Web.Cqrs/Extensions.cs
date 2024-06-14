namespace Proget.Web.Cqrs;

public static class EndpointRouteBuilderExtensions
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
        var routeHandlerBuilder = builder.MapPost(route, async (TCommand command, IValidator<TCommand> validator, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            command = preprocess(command);

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            
            await commandDispatcher.SendAsync(command, cancellationToken);
            
            var response = postprocess(command);
            return Results.Ok(response);
        });

        configureEndpoint(routeHandlerBuilder);

        return builder;
    }
}