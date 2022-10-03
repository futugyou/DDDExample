namespace Example.Domain.UnitTest;
public class StubEventSourcing: AggregateRoot
{
    public void ExposeAddDomainEvent(DomainEvent stubEvent, long originalVersion)
    {
        AddDomainEvent(stubEvent, originalVersion);
    }
}
