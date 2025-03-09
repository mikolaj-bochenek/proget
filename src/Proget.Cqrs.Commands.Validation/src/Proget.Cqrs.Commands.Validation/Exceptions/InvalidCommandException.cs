namespace Proget.Cqrs.Commands.Validation.Exceptions;

public class InvalidCommandException : BadRequestException
{
    public IReadOnlyList<ValidationFailure> Errors { get; }
    
    public InvalidCommandException(IEnumerable<ValidationFailure> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public override string ToString()
    {
        return $"{Message}\n{string.Join("\n", Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))}";
    }
}