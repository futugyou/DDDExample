namespace Example.Domain.Core;

public class CustomerNameCheckException : Exception
{
    public CustomerNameCheckException(string message) : base(message)
    {
    }
}
