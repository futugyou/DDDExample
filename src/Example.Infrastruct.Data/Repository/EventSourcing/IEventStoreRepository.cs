
using Example.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Infrastruct.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        Task Store(StoredEvent storedEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
