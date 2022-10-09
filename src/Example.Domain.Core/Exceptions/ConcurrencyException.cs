namespace Example.Domain.Core;

public class ConcurrencyException : Exception
{
    public ConcurrencyException(string message) : base(message)
    {
    }
}
