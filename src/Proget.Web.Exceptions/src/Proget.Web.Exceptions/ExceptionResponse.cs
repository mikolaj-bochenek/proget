namespace Proget.Web.Exceptions;

public record ExceptionResponse(
    string Message,
    HttpStatusCode StatusCode,
    int Code
);
