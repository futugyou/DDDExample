namespace Example.Application;

public class ChangeCustomerNameValidatation : CustomerValidation<ChangeCustomerNameCommand>
{
    public ChangeCustomerNameValidatation()
    {
        ValidateName();
    }

    protected void ValidateName()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("can not be null");
    }
}
