using MediatR;

namespace Producao.Application.Queries;

/// <summary>
/// Query para buscar pedidos em produção (visão da cozinha)
/// </summary>
public class GetOrdersInProductionQuery : IRequest<GetOrdersInProductionQueryResult>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Status { get; set; }
}

public class GetOrdersInProductionQueryResult
{
    public List<ProductionOrderItem> Orders { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}

public class ProductionOrderItem
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
    public int ItemsCount { get; set; }
}

