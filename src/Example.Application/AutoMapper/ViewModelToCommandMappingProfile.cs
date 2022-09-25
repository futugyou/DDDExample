using AutoMapper;
using Example.Application.ViewModels;
using Example.Domain.Commands.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Application.AutoMapper
{
    public class ViewModelToCommandMappingProfile : Profile
    {
        public ViewModelToCommandMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterCustomerCommand>()
                .ConstructUsing(x => new RegisterCustomerCommand(x.Name, x.Email, x.BirthDate));
        }
    }
}
