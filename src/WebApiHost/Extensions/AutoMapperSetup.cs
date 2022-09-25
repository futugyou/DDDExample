namespace WebApiHost;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        //添加服务
        services.AddAutoMapper(typeof(Startup),typeof(AutoMapperConfig));
        //启动配置
        AutoMapperConfig.RegisterMapper();
    }
}
