namespace Proget.Exceptions;

public abstract class CoreException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    public abstract int Code { get; }

    public CoreException(string message) : base(message)
    {
    }

    public CoreException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public CoreException() : base()
    {
    }
}
