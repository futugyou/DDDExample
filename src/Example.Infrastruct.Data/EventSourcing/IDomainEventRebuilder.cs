using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastruct.Data;
public interface IDomainEventRebuilder
{
    IEnumerable<IDomainEvent> RebuildDomainEvents(IEnumerable<EventStore> events);
}
