namespace Proget.Web.Cqrs
{
    public static class GetExtensions
    {
        public static IEndpointRouteBuilder MapGetAsParamsCqrs<TQuery, TResult, TResponse>(
            this IEndpointRouteBuilder builder,
            string route,
            Func<TQuery, TQuery>? preprocess = null,
            Func<TResult, TResponse>? postprocess = null,
            Action<RouteHandlerBuilder>? configureEndpoint = null)
            where TQuery : class, IQuery<TResult>
            where TResponse : class
        {
            var routeHandlerBuilder = builder.MapGet(route, async ([AsParameters] TQuery query, IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            {
                query = preprocess?.Invoke(query) ?? query;

                var result = await dispatcher.QueryAsync(query, cancellationToken);

                var response = postprocess?.Invoke(result);
                return Results.Ok(response);
            });

            configureEndpoint?.Invoke(routeHandlerBuilder);

            return builder;
        }

        public static IEndpointRouteBuilder MapGetCqrs<TQuery, TResult, TResponse>(
            this IEndpointRouteBuilder builder,
            string route,
            Func<TQuery, TQuery>? preprocess = null,
            Func<TResult, TResponse>? postprocess = null,
            Action<RouteHandlerBuilder>? configureEndpoint = null)
            where TQuery : class, IQuery<TResult>
            where TResponse : class
        {
            var routeHandlerBuilder = builder.MapGet(route, async ([FromQuery] TQuery query, IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            {
                query = preprocess?.Invoke(query) ?? query;

                var result = await dispatcher.QueryAsync(query, cancellationToken);

                var response = postprocess?.Invoke(result);
                return Results.Ok(response);
            });

            configureEndpoint?.Invoke(routeHandlerBuilder);

            return builder;
        }

        public static IEndpointRouteBuilder MapGetByIdCqrs<TQuery, TResult, TResponse, TId>(
            this IEndpointRouteBuilder builder,
            string route,
            Func<TQuery, TId, TQuery>? preprocess = null,
            Func<TResult, TResponse>? postprocess = null,
            Action<RouteHandlerBuilder>? configureEndpoint = null)
            where TQuery : class, IQuery<TResult>
            where TResponse : class
            where TId : notnull
        {
            var routeHandlerBuilder = builder.MapGet(route, async ([FromRoute] TId id, [FromQuery] TQuery query, IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            {
                query = preprocess?.Invoke(query, id) ?? query;

                var result = await dispatcher.QueryAsync(query, cancellationToken);

                var response = postprocess?.Invoke(result);
                return Results.Ok(response);
            });

            configureEndpoint?.Invoke(routeHandlerBuilder);

            return builder;
        }
    }
}