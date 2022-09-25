namespace Example.Application;

public class ViewModelToDomainMappingProfile : Profile
{
    /// <summary>
    /// 配置构造函数，用来创建关系映射
    /// </summary>
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<CustomerViewModel, Customer>()
            .ForPath(a => a.Address.City, b => b.MapFrom(c => c.City))
            .ForPath(a => a.Address.County, b => b.MapFrom(c => c.County))
            .ForPath(a => a.Address.Province, b => b.MapFrom(c => c.Province))
            .ForPath(a => a.Address.Street, b => b.MapFrom(c => c.Street));
    }
}
