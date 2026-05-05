using CrossCutting.Configuration;
using Domain.Entities.v1;
using Domain.Interfaces.v1.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Data.MongoDb.v1.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly IMongoCollection<Sale> _collection;

    public SaleRepository(
        IMongoClient client,
        IOptions<AppSettings> settings
    )
    {
        var mongo = settings.Value.Mongo;

        if (mongo == null)
            throw new ArgumentNullException(nameof(settings));

        var connection = mongo.ConnectionSettings
            .First(x => x.Id == "mongoDb");

        if (string.IsNullOrEmpty(connection.DatabaseName))
            throw new ArgumentException("Mongo database not configured");

        if (string.IsNullOrEmpty(mongo.Collection))
            throw new ArgumentException("Mongo collection not configured");

        var database = client.GetDatabase(
            connection.DatabaseName
        );

        _collection = database.GetCollection<Sale>(
            mongo.Collection
        );
    }
    public async Task AddAsync(
        Sale sale
    ) => await _collection.InsertOneAsync(sale);

    public async Task<Sale?> GetByIdAsync(
        Guid id
    ) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task UpdateAsync(
        Sale sale
    )
    {
        var result = await _collection.ReplaceOneAsync(
            x => x.Id == sale.Id,
            sale
        );

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Sale {sale.Id} not found");
    }

    public async Task DeleteAsync(
        Guid id
    )
    {
        var result = await _collection.DeleteOneAsync(
            x => x.Id == id
        );

        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"Sale {id} not found");
    }
}