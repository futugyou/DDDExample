using System;
using System.Collections.Generic;
using System.Text;
using Example.Domain.Core.Events;
using Example.Infrastruct.Data.Repository.EventSourcing;
using Newtonsoft.Json;

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
            var serializedData = JsonConvert.SerializeObject(@event);
            var storedEvent = new StoredEvent(@event, serializedData, "");
            _eventStoreRepository.Store(storedEvent);
        }
    }
}
