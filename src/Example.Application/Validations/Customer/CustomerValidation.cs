namespace Example.Application;

public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
{

}
