namespace Proget.Exceptions;

public abstract class NotFoundException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public override int Code => (int) HttpStatusCode.NotFound;

    public NotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException() : base()
    {
    }
}
