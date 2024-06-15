namespace Proget.Exceptions;

public abstract class ConflictException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public override int Code => (int) HttpStatusCode.Conflict;

    public ConflictException(string message, Exception inner) : base(message, inner)
    {
    }

    public ConflictException(string message) : base(message)
    {
    }
    
    public ConflictException() : base()
    {
    }
}
