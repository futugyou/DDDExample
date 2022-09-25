using FluentValidation.Results;
using MediatR;
using System;

namespace Christ.Domain.Core.Commands
{

    public abstract class Command : IRequest
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
        public abstract bool IsValid();
    }
}