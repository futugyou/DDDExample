using Example.Domain.Commands.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Validations.Customer
{
    public class RegisterCustomerValidatation : CustomerValidation<CustomerCommand>
    {
        public RegisterCustomerValidatation()
        {
            ValidateName();
            ValidateEmail();
        }
    }
}
