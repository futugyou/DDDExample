namespace Example.Application;

public class RegisterCustomerValidatation : CustomerValidation<CustomerCommand>
{
    public RegisterCustomerValidatation()
    {
        ValidateName();
        ValidateEmail();
    }
}
