namespace Example.Application;

public class RegisterCustomerCommand : CustomerCommand
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public DateTime BirthDate { get; protected set; }
    public RegisterCustomerCommand(Guid id, string name, string email, DateTime birthDate)
    {
        Id = id;
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
