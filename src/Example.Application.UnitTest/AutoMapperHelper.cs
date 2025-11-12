using Microsoft.Extensions.Logging.Abstractions;

namespace Example.Application.UnitTest;

public class AutoMapperHelper
{
    public static readonly IMapper mapper = GetMapper();

    private static IMapper GetMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DomainToViewModelMappingProfile());
            mc.AddProfile(new ViewModelToDomainMappingProfile());
            mc.AddProfile(new ViewModelToCommandMappingProfile());
        }, new NullLoggerFactory());

        return mappingConfig.CreateMapper();
    }
}
