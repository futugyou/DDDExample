using Example.Domain.Commands.Customer;
using Example.Domain.Core.Bus;
using Example.Domain.Events.Customer;
using Example.Domain.Interfaces;
using Example.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand>, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICustomerRepository _customerRepository;
        public CustomerCommandHandler(IUnitOfWork unitOfWork, IMediatorHandler mediatorHandler, ICustomerRepository customerRepository)
            : base(unitOfWork, mediatorHandler)
        {
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
            _customerRepository = customerRepository;
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }

        public Task<Unit> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(Unit.Value);
            }
            var customer = new Customer(Guid.NewGuid(), request.Name, request.Email, request.BirthDate);
            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                //DOTO;
                return Task.FromResult(Unit.Value);
            }
            _customerRepository.Add(customer);
            if (true)
            {
                _mediatorHandler.RaiseEvent(new CustomerRegisterEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
            }
            _unitOfWork.Commit();
            return Task.FromResult(Unit.Value);
        }
    }
}
