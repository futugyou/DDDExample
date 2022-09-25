namespace Example.Domain;

public class RegisterCustomerValidatation : CustomerValidation<CustomerCommand>
{
    public RegisterCustomerValidatation()
    {
        ValidateName();
        ValidateEmail();
    }
}
