namespace Proget.Exceptions;

public abstract class ForbiddenException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public override int Code => (int) HttpStatusCode.Forbidden;

    public ForbiddenException(string message, Exception inner) : base(message, inner)
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException() : base()
    {
    }
}
