namespace Example.Application;

public class AutoMapperConfig
{
    public static MapperConfiguration RegisterMapper()
    {
        return new MapperConfiguration(conf =>
        {
            conf.AddProfile(new DomainToViewModelMappingProfile());
            conf.AddProfile(new ViewModelToDomainMappingProfile());
            conf.AddProfile(new ViewModelToCommandMappingProfile());
        });
    }
}