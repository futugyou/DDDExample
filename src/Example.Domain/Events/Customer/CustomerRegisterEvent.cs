namespace Example.Domain;

public class CustomerRegisterEvent : DomainEvent
{
    public CustomerRegisterEvent(Guid id, string name, string email, DateTime brithDate, CustomerLevel customerLevel)
    {
        AggregateId = id;
        Name = name;
        Email = email;
        BirthDate = brithDate;
        CustomerLevel = customerLevel;
    }

    public string Name { get; }
    public string Email { get; }
    public DateTime BirthDate { get; }
    public CustomerLevel CustomerLevel { get; }
}
