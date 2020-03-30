using Example.Domain.Core.Events;
using Example.Infrastruct.Data.Repository.EventSourcing;
using System.Text.Json;

namespace Example.Infrastruct.Data.EventStore
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }
        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(@event);
            var storedEvent = new StoredEvent(@event, serializedData, "");
            _eventStoreRepository.Store(storedEvent);
        }
    }
}
