using AutoMapper;

namespace Example.Application.AutoMap
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(conf =>
            {
                conf.AddProfile(new DomainToViewModelMappingProfile());
            });
        }
    }
}