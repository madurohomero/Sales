using Domain.Interfaces.v1.Repositories;
using Infrastructure.Data.MongoDb.v1.Repositories;

namespace Sales.Api.Infrastructure.DependencyInjectors;

public static class RepositoryInjector
{
    public static void RegisterRepositories(
        this IServiceCollection services
    )
    {
        services.AddScoped<ISaleRepository, SaleRepository>();
    }
}