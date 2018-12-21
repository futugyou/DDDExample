using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Core.Events
{
    public abstract class Message : IRequest
    {
        public Guid AggregateId { get; set; }
        public string MessageType { get; set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
