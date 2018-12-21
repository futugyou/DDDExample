using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Application.Interfaces;
using Example.Application.ViewModels;
using Example.Domain.Commands.Customer;
using Example.Domain.Core.Bus;
using Example.Domain.Interfaces;
using Example.Domain.Models;

namespace Example.Application.Services
{
    /// <summary>
    /// CustomerAppService 服务接口实现类,继承 服务接口
    /// 通过 DTO 实现视图模型和领域模型的关系处理
    /// 作为调度者，协调领域层和基础层，
    /// 这里只是做一个面向用户用例的服务接口,不包含业务规则或者知识
    /// </summary>
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        public CustomerAppService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IMediatorHandler mediatorHandler)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>();
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }

        public void Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterCustomerCommand>(customerViewModel);
            //command bus
            _mediatorHandler.SendCommand(registerCommand);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            _customerRepository.Update(_mapper.Map<Customer>(customerViewModel));
        }

        public void Remove(Guid id)
        {
            _customerRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
