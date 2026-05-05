using Domain.DomainServices;
using Domain.Interfaces.v1.DomainServices;

namespace Sales.Api.Infrastructure.DependencyInjectors;

public static class DomainServicesInjector
{
    public static void RegisterDomainservices(
    this IServiceCollection services
)
    {
        services.AddScoped<ISaleDomainService, SaleDomainService>();
    }
}
