namespace Proget.Web.Exceptions.Mappers;

internal sealed class DefaultExceptionMapper : IExceptionMapper
{
    public ExceptionResponse? Map(Exception exception)
        => exception switch
        {
            BadRequestException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            ConflictException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            ForbiddenException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            NotFoundException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            RequestTimeoutException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            UnauthorizedException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            InternalServerException ex => new ExceptionResponse(ex.Message, ex.StatusCode, ex.Code),
            _ => null
        };
}
