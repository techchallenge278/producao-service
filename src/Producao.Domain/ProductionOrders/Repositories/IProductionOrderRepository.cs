using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.ValueObjects;
using Producao.Domain.Shared.Repositories;

namespace Producao.Domain.ProductionOrders.Repositories;

/// <summary>
/// Interface para repositório de pedidos em produção
/// </summary>
public interface IProductionOrderRepository : IRepository<ProductionOrder>
{
    /// <summary>
    /// Busca um pedido pelo OrderId
    /// </summary>
    Task<ProductionOrder?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca pedidos por status
    /// </summary>
    Task<IEnumerable<ProductionOrder>> GetByStatusAsync(ProductionStatus status, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca pedidos em produção (Received ou InPreparation)
    /// </summary>
    Task<IEnumerable<ProductionOrder>> GetOrdersInProductionAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca pedidos prontos para retirada
    /// </summary>
    Task<IEnumerable<ProductionOrder>> GetReadyOrdersAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}

