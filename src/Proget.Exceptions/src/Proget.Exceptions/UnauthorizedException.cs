namespace Proget.Exceptions;

public abstract class UnauthorizedException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public override int Code => (int) HttpStatusCode.Unauthorized;

    public UnauthorizedException(string message, Exception inner) : base(message, inner)
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException() : base()
    {
    }
}
