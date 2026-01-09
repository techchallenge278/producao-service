using MediatR;

namespace Producao.Application.Commands;

/// <summary>
/// Comando para atualizar o status de um pedido em produção
/// </summary>
public class UpdateProductionOrderStatusCommand : IRequest<UpdateProductionOrderStatusCommandResult>
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class UpdateProductionOrderStatusCommandResult
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public bool NotificationSent { get; set; }
}

