using Sales.Api.Infrastructure.DependencyInjectors;

namespace Sales.Api.Infrastructure.IoC;

public static class Bootstrapper
{
    public static void Register(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.RegisterMongo(
            configuration
        );
        services.RegisterMediatR();
        services.RegisterAutoMapper();
        services.RegisterRepositories();
        services.RegisterDomainservices();
    }
}