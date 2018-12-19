using AutoMapper;
using Example.Application.AutoMapper;

namespace Example.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(conf =>
            {
                conf.AddProfile(new DomainToViewModelMappingProfile());
                conf.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}