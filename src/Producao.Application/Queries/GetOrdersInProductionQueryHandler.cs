using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Producao.Application.Queries;

public class GetOrdersInProductionQueryHandler : IRequestHandler<GetOrdersInProductionQuery, GetOrdersInProductionQueryResult>
{
    private readonly IProductionOrderRepository _repository;
    private readonly ILogger<GetOrdersInProductionQueryHandler> _logger;

    public GetOrdersInProductionQueryHandler(
        IProductionOrderRepository repository,
        ILogger<GetOrdersInProductionQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<GetOrdersInProductionQueryResult> Handle(GetOrdersInProductionQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Buscando pedidos em produção. Status: {Status}, Página: {Page}", request.Status, request.PageNumber);

        IEnumerable<Domain.ProductionOrders.Entities.ProductionOrder> orders;

        if (!string.IsNullOrWhiteSpace(request.Status) && 
            Enum.TryParse<ProductionStatus>(request.Status, true, out var status))
        {
            orders = await _repository.GetByStatusAsync(status, request.PageNumber, request.PageSize, cancellationToken);
        }
        else
        {
            orders = await _repository.GetOrdersInProductionAsync(request.PageNumber, request.PageSize, cancellationToken);
        }

        var orderItems = orders.Select(o => new ProductionOrderItem
        {
            Id = o.Id,
            OrderId = o.OrderId,
            CustomerId = o.CustomerId,
            CustomerName = o.CustomerName,
            TotalPrice = o.TotalPrice,
            Status = o.Status.ToString(),
            EstimatedMinutes = o.EstimatedMinutes,
            WaitingTimeMinutes = o.GetWaitingTimeMinutes(),
            CreatedAt = o.CreatedAt,
            StartedAt = o.StartedAt,
            ReadyAt = o.ReadyAt,
            ItemsCount = o.Items.Count
        }).ToList();

        return new GetOrdersInProductionQueryResult
        {
            Orders = orderItems,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = orderItems.Count
        };
    }
}

