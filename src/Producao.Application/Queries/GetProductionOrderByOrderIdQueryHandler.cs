using Producao.Application.Common.Exceptions;
using Producao.Domain.ProductionOrders.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Producao.Application.Queries;

public class GetProductionOrderByOrderIdQueryHandler : IRequestHandler<GetProductionOrderByOrderIdQuery, GetProductionOrderByOrderIdQueryResult>
{
    private readonly IProductionOrderRepository _repository;
    private readonly ILogger<GetProductionOrderByOrderIdQueryHandler> _logger;

    public GetProductionOrderByOrderIdQueryHandler(
        IProductionOrderRepository repository,
        ILogger<GetProductionOrderByOrderIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GetProductionOrderByOrderIdQueryResult> Handle(GetProductionOrderByOrderIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando pedido em produção pelo OrderId {OrderId}", request.OrderId);

        var productionOrder = await _repository.GetByOrderIdAsync(request.OrderId, cancellationToken);

        if (productionOrder == null)
        {
            _logger.LogWarning("Pedido em produção não encontrado para OrderId {OrderId}", request.OrderId);
            return new GetProductionOrderByOrderIdQueryResult
            {
                Success = false,
                Error = $"Pedido em produção não encontrado para OrderId {request.OrderId}"
            };
        }

        return new GetProductionOrderByOrderIdQueryResult
        {
            Success = true,
            Order = new ProductionOrderDto
            {
                Id = productionOrder.Id,
                OrderId = productionOrder.OrderId,
                CustomerId = productionOrder.CustomerId,
                CustomerName = productionOrder.CustomerName,
                TotalPrice = productionOrder.TotalPrice,
                Status = productionOrder.Status.ToString(),
                EstimatedMinutes = productionOrder.EstimatedMinutes,
                WaitingTimeMinutes = productionOrder.GetWaitingTimeMinutes(),
                CreatedAt = productionOrder.CreatedAt,
                StartedAt = productionOrder.StartedAt,
                ReadyAt = productionOrder.ReadyAt,
                CompletedAt = productionOrder.CompletedAt,
                Items = productionOrder.Items.Select(item => new ProductionOrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            }
        };
    }
}

