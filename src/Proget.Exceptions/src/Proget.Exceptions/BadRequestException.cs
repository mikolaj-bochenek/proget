namespace Proget.Exceptions;

public abstract class BadRequestException : CoreException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public override int Code => (int) HttpStatusCode.BadRequest;

    public BadRequestException(string message, Exception inner) : base(message, inner)
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException() : base()
    {
    }
}
