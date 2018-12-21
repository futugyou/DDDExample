using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Domain.Core.Events;
using Example.Infrastruct.Data.Context;

namespace Example.Infrastruct.Data.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreSQLContext _eventStoreSQLContext;
        public EventStoreRepository(EventStoreSQLContext eventStoreSQLContext)
        {
            _eventStoreSQLContext = eventStoreSQLContext;
        }
        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _eventStoreSQLContext.StoredEvents.Where(p => p.AggregateId == aggregateId).ToList();
        }

        public void Dispose()
        {
            _eventStoreSQLContext.Dispose();
        }

        public void Store(StoredEvent storedEvent)
        {
            _eventStoreSQLContext.Add(storedEvent);
            _eventStoreSQLContext.SaveChanges();
        }
    }
}
