﻿namespace Example.Application;

public class ViewModelToCommandMappingProfile : Profile
{
    public ViewModelToCommandMappingProfile()
    {
        CreateMap<CustomerViewModel, RegisterCustomerCommand>()
            .ConstructUsing(x => new RegisterCustomerCommand(x.Name, x.Email, x.BirthDate));
    }
}
