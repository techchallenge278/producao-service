namespace Producao.Domain.ProductionOrders.Services;

/// <summary>
/// Interface para serviço de notificação
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Notifica mudança de status do pedido
    /// </summary>
    Task NotifyOrderStatusChangeAsync(Guid orderId, string previousStatus, string newStatus, Guid? customerId);

    /// <summary>
    /// Notifica que o pedido está pronto para retirada
    /// </summary>
    Task NotifyOrderReadyAsync(Guid orderId, Guid? customerId);
}

