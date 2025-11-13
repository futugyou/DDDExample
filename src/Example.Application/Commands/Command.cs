namespace Example.Application;

public abstract class Command : IRequest
{
    public DateTime Timestamp { get; private set; }

    public FluentValidation.Results.ValidationResult? ValidationResult { get; set; }
    protected Command()
    {
        Timestamp = DateTime.Now;
    }
    public abstract bool IsValid();
}