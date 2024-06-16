namespace Proget.Web.Exceptions;

internal sealed class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionMapper _exceptionMapper;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(
        IExceptionMapper exceptionMapper,
        ILogger<ErrorHandlerMiddleware> logger
    )
    {
        _exceptionMapper = exceptionMapper;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = _exceptionMapper.Map(exception)
            ?? throw exception;

        context.Response.StatusCode = exceptionResponse.Code;
        context.Response.ContentType = "application/json";
        
        await context.Response.WriteAsJsonAsync(exceptionResponse);
    }
}
