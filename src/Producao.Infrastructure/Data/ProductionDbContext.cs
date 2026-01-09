using Producao.Domain.ProductionOrders.Entities;
using MongoDB.Driver;

namespace Producao.Infrastructure.Data;

/// <summary>
/// Contexto MongoDB para o Production Service
/// </summary>
public class ProductionDbContext
{
    private readonly IMongoDatabase _database;

    public ProductionDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<ProductionOrder> ProductionOrders =>
        _database.GetCollection<ProductionOrder>("ProductionOrders");
}
