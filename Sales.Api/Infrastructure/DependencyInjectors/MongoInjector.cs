using CrossCutting.Configuration;
using MongoDB.Driver;

namespace Sales.Api.Infrastructure.DependencyInjectors;

public static class MongoInjector
{
    public static void RegisterMongo(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IMongoClient>(_ =>
        {
            var settings = configuration.Get<AppSettings>();

            if (settings?.Mongo?.ConnectionSettings == null)
                throw new Exception("Mongo configuration is missing.");

            var connection = settings.Mongo.ConnectionSettings
                .First(x => x.Id == "mongoDb");

            return new MongoClient(
                connection.ConnectionString
            );
        });
    }
}