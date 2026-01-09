using MediatR;

namespace Producao.Application.Queries;

/// <summary>
/// Query para buscar um pedido em produção pelo OrderId
/// </summary>
public class GetProductionOrderByOrderIdQuery : IRequest<GetProductionOrderByOrderIdQueryResult>
{
    public Guid OrderId { get; set; }
}

public class GetProductionOrderByOrderIdQueryResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public ProductionOrderDto? Order { get; set; }
}

public class ProductionOrderDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public int WaitingTimeMinutes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? ReadyAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public List<ProductionOrderItemDto> Items { get; set; } = new();
}

public class ProductionOrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

