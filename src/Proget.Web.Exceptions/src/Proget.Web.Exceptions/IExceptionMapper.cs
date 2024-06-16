namespace Proget.Web.Exceptions;

public interface IExceptionMapper
{
    ExceptionResponse? Map(Exception exception);
}
