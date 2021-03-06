using Example.Domain.Validations.Customer;
using System;

namespace Example.Domain.Commands.Customer
{
    public class RegisterCustomerCommand : CustomerCommand
    {
        public RegisterCustomerCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerValidatation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
