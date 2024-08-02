namespace Example.Domain;

public record ChangeCustomerLevelEvent : DomainEvent
{
    public ChangeCustomerLevelEvent(CustomerLevel customerLevel)
    {
        CustomerLevel = customerLevel;
    }

    public CustomerLevel CustomerLevel { get; }
}
