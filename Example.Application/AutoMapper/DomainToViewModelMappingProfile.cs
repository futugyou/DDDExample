using System;
using AutoMapper;
using Example.Application.ViewModels;
using Example.Domain.Models;

namespace Example.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(d => d.Province, c => c.MapFrom(a => a.Address.Province))
                .ForMember(d => d.City, c => c.MapFrom(a => a.Address.City))
                .ForMember(d => d.County, c => c.MapFrom(a => a.Address.County))
                .ForMember(d => d.Street, c => c.MapFrom(a => a.Address.Street));
        }
    }
}
