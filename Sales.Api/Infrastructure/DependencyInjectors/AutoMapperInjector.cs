using Domain.MapperProfiles.v1;

namespace Sales.Api.Infrastructure.DependencyInjectors;

public static class AutoMapperInjector
{
    public static void RegisterAutoMapper(
        this IServiceCollection services
    )
    {
        services.AddAutoMapper(
            typeof(SalesProfile).Assembly
        );
    }
}