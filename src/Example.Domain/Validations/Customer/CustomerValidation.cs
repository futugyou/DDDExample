namespace Example.Domain;

public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
{
    protected void ValidateName()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("can not be null");
    }

    protected void ValidateEmail()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage("Email not be null");
    }
}
