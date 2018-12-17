using System;
using AutoMapper;
using Example.Application.ViewModels;
using Example.Domain.Models;

namespace Example.Application.AutoMap
{
    public class DomainToViewModelMappingProfile: Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
