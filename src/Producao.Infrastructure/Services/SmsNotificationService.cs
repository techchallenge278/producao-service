using Producao.Domain.ProductionOrders.Services;
using Microsoft.Extensions.Logging;

namespace Producao.Infrastructure.Services;

/// <summary>
/// Implementação do serviço de notificação por SMS
/// </summary>
public class SmsNotificationService : INotificationService
{
    private readonly ILogger<SmsNotificationService> _logger;

    public SmsNotificationService(ILogger<SmsNotificationService> logger)
    {
        _logger = logger;
    }

    public async Task NotifyOrderStatusChangeAsync(Guid orderId, string previousStatus, string newStatus, Guid? customerId)
    {
        if (customerId == null)
        {
            _logger.LogInformation("Pedido {OrderId} é anônimo, não será enviada notificação", orderId);
            return;
        }

        _logger.LogInformation(
            "Enviando SMS para cliente {CustomerId} sobre mudança de status do pedido {OrderId}: {PreviousStatus} -> {NewStatus}",
            customerId, orderId, previousStatus, newStatus);

        // Simular tempo de processamento do envio de SMS
        await Task.Delay(100);
        
        _logger.LogInformation(
            "SMS enviado para o cliente {CustomerId} sobre o pedido {OrderId}",
            customerId, orderId);
    }

    public async Task NotifyOrderReadyAsync(Guid orderId, Guid? customerId)
    {
        if (customerId == null)
        {
            _logger.LogInformation("Pedido {OrderId} é anônimo, não será enviada notificação", orderId);
            return;
        }

        _logger.LogInformation(
            "Enviando SMS para cliente {CustomerId} que o pedido {OrderId} está pronto para retirada",
            customerId, orderId);

        // Simular tempo de processamento do envio de SMS
        await Task.Delay(100);
        
        _logger.LogInformation(
            "SMS de pedido pronto enviado para o cliente {CustomerId} sobre o pedido {OrderId}",
            customerId, orderId);
    }
}

