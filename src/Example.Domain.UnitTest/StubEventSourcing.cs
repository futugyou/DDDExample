namespace Example.Domain.UnitTest;
public class StubEventSourcing: AggregateRoot
{
    public void ExposeAddDomainEvent(IDomainEvent stubEvent, long originalVersion)
    {
        AddDomainEvent(stubEvent, originalVersion);
    }
}
