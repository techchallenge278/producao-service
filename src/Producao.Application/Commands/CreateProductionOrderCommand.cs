using MediatR;

namespace Producao.Application.Commands;

/// <summary>
/// Comando para criar um pedido em produção
/// </summary>
public class CreateProductionOrderCommand : IRequest<CreateProductionOrderCommandResult>
{
    public Guid OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CreateProductionOrderItemCommand> Items { get; set; } = new();
}

public class CreateProductionOrderItemCommand
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CreateProductionOrderCommandResult
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public DateTime CreatedAt { get; set; }
}

