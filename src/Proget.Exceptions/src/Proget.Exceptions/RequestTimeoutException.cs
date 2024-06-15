namespace Proget.Exceptions;

public abstract class RequestTimeoutException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.RequestTimeout;

    public override int Code => (int) HttpStatusCode.RequestTimeout;

    public RequestTimeoutException(string message, Exception inner) : base(message, inner)
    {
    }

    public RequestTimeoutException(string message) : base(message)
    {
    }

    public RequestTimeoutException() : base()
    {
    }
}
