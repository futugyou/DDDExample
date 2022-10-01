namespace Example.Domain;

public class CustomerRegisterEvent : DomainEvent
{
    public CustomerRegisterEvent(Guid id, string name, string email, DateTime brithDate)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = brithDate;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
}
