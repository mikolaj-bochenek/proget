namespace Proget.Web.Cqrs
{
    public static class GetExtensions
    {
        public static IEndpointRouteBuilder MapGetCqrs<TQuery, TResult, TResponse>(
            this IEndpointRouteBuilder builder,
            string route,
            Func<TResult, TResponse> postprocess,
            Action<RouteHandlerBuilder> configureEndpoint)
            where TQuery : class, IQuery<TResult>
            where TResponse : class
        {
            var routeHandlerBuilder = builder.MapGet(route, async ([AsParameters] TQuery query, IValidator<TQuery> validator, IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(query, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await dispatcher.QueryAsync(query, cancellationToken);

                var response = postprocess(result);
                return Results.Ok(response);
            });

            configureEndpoint(routeHandlerBuilder);

            return builder;
        }
    }
}