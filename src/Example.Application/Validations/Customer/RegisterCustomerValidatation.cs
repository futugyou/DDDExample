namespace Example.Application;

public class RegisterCustomerValidatation : CustomerValidation<RegisterCustomerCommand>
{
    public RegisterCustomerValidatation()
    {
        ValidateName();
        ValidateEmail();
    }

    protected void ValidateName()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("can not be null");
    }

    protected void ValidateEmail()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage("Email not be null");
    }
}
