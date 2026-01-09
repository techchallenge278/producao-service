using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using Producao.Infrastructure.Data;
using MongoDB.Driver;

namespace Producao.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de pedidos em produção usando MongoDB
/// </summary>
public class ProductionOrderRepository : IProductionOrderRepository
{
    private readonly IMongoCollection<ProductionOrder> _collection;

    public ProductionOrderRepository(ProductionDbContext context)
    {
        _collection = context.ProductionOrders;
    }

    public async Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductionOrder>.Filter.Eq(p => p.Id, id);

        var options = new FindOptions<ProductionOrder, ProductionOrder> { Limit = 1 };
        using var cursor = await _collection.FindAsync(filter, options, cancellationToken);
        return await FirstOrDefaultFromCursorAsync(cursor, cancellationToken);
    }

    public async Task<IEnumerable<ProductionOrder>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductionOrder>.Filter.Empty;

        var options = new FindOptions<ProductionOrder, ProductionOrder>
        {
            Sort = Builders<ProductionOrder>.Sort.Descending(p => p.CreatedAt),
            Skip = (pageNumber - 1) * pageSize,
            Limit = pageSize
        };

        using var cursor = await _collection.FindAsync(filter, options, cancellationToken);
        return await ToListFromCursorAsync(cursor, cancellationToken);
    }

    public async Task<ProductionOrder> CreateAsync(ProductionOrder entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ProductionOrder entity, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(
            p => p.Id == entity.Id,
            entity,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(ProductionOrder entity, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(p => p.Id == entity.Id, cancellationToken);
    }

    public async Task<ProductionOrder?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductionOrder>.Filter.Eq(p => p.OrderId, orderId);

        var options = new FindOptions<ProductionOrder, ProductionOrder> { Limit = 1 };
        using var cursor = await _collection.FindAsync(filter, options, cancellationToken);
        return await FirstOrDefaultFromCursorAsync(cursor, cancellationToken);
    }

    private static async Task<ProductionOrder?> FirstOrDefaultFromCursorAsync(IAsyncCursor<ProductionOrder> cursor, CancellationToken cancellationToken)
    {
        if (await cursor.MoveNextAsync(cancellationToken))
        {
            return cursor.Current.FirstOrDefault();
        }

        return null;
    }

    private static async Task<List<ProductionOrder>> ToListFromCursorAsync(IAsyncCursor<ProductionOrder> cursor, CancellationToken cancellationToken)
    {
        var result = new List<ProductionOrder>();

        while (await cursor.MoveNextAsync(cancellationToken))
        {
            result.AddRange(cursor.Current);
        }

        return result;
    }

    public async Task<IEnumerable<ProductionOrder>> GetByStatusAsync(ProductionStatus status, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductionOrder>.Filter.Eq(p => p.Status, status);

        var options = new FindOptions<ProductionOrder, ProductionOrder>
        {
            Sort = Builders<ProductionOrder>.Sort.Descending(p => p.CreatedAt),
            Skip = (pageNumber - 1) * pageSize,
            Limit = pageSize
        };

        using var cursor = await _collection.FindAsync(filter, options, cancellationToken);
        return await ToListFromCursorAsync(cursor, cancellationToken);
    }

    public async Task<IEnumerable<ProductionOrder>> GetOrdersInProductionAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var statusFilter = Builders<ProductionOrder>.Filter.In(
            p => p.Status,
            new[] { ProductionStatus.Received, ProductionStatus.InPreparation });

        var options = new FindOptions<ProductionOrder, ProductionOrder>
        {
            Sort = Builders<ProductionOrder>.Sort.Descending(p => p.CreatedAt),
            Skip = (pageNumber - 1) * pageSize,
            Limit = pageSize
        };

        using var cursor = await _collection.FindAsync(statusFilter, options, cancellationToken);
        return await ToListFromCursorAsync(cursor, cancellationToken);
    }

    public async Task<IEnumerable<ProductionOrder>> GetReadyOrdersAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var filter = Builders<ProductionOrder>.Filter.Eq(p => p.Status, ProductionStatus.Ready);

        var options = new FindOptions<ProductionOrder, ProductionOrder>
        {
            Sort = Builders<ProductionOrder>.Sort.Descending(p => p.ReadyAt),
            Skip = (pageNumber - 1) * pageSize,
            Limit = pageSize
        };

        using var cursor = await _collection.FindAsync(filter, options, cancellationToken);
        return await ToListFromCursorAsync(cursor, cancellationToken);
    }
}

