namespace Example.Domain;

public class ChangeCustomerLevelEvent : DomainEvent
{
    public ChangeCustomerLevelEvent(CustomerLevel customerLevel)
    {
        CustomerLevel = customerLevel;
    }

    public CustomerLevel CustomerLevel { get; }
}
