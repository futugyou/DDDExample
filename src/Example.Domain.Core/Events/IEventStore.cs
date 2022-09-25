namespace Example.Domain.Core;

public interface IEventStore
{
    Task Save<T>(T theEvent) where T : Event;
}
