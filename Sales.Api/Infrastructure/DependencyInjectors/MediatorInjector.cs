using Domain.Commands.v1.Creates;
using MediatR;

namespace Sales.Api.Infrastructure.DependencyInjectors;

public static class MediatorInjector
{
    public static void RegisterMediatR(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(CreateSaleCommandHandler).Assembly
            );
        });

        services.AddTransient(
           typeof(IPipelineBehavior<,>),
           typeof(LoggingBehavior<,>)
       );
    }
}