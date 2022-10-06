namespace Example.Application;
public class ChangeCustomerNameCommand : CustomerCommand
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }

    public ChangeCustomerNameCommand(Guid id,string name)
    {
        Id = id;
        Name = name;
    }

    public override bool IsValid()
    {
        ValidationResult = new ChangeCustomerNameValidatation().Validate(this);
        return ValidationResult.IsValid;
    }
}
