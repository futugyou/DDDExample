using Christ.Domain.Core.Commands;
using Example.Domain.Core.Bus;
using Example.Domain.Core.Notifications;
using Example.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;
        public CommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler)
        {
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
        }


        protected void NotifyValidationErrors(Command command)
        {
            foreach (var item in command.ValidationResult.Errors)
            {
                _mediatorHandler.RaiseEvent(new DomainNotification("", item.ErrorMessage));
            }
        }

        public bool Commit() => _unitOfWork.Commit();

    }
}
