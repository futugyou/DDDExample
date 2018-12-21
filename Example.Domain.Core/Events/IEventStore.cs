using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
