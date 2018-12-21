
using Example.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Infrastruct.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent storedEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
