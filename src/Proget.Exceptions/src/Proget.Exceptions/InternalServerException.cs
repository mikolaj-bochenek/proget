namespace Proget.Exceptions;

public abstract class InternalServerException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

    public override int Code => (int) HttpStatusCode.InternalServerError;

    public InternalServerException(string message, Exception inner) : base(message, inner)
    {
    }

    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException() : base()
    {
    }
}
