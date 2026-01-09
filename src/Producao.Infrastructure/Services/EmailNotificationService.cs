using Producao.Domain.ProductionOrders.Services;
using Microsoft.Extensions.Logging;

namespace Producao.Infrastructure.Services;

/// <summary>
/// Implementação do serviço de notificação por email
/// </summary>
public class EmailNotificationService : INotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(ILogger<EmailNotificationService> logger)
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
            "Notificando cliente {CustomerId} sobre mudança de status do pedido {OrderId}: {PreviousStatus} -> {NewStatus}",
            customerId, orderId, previousStatus, newStatus);

        string emailSubject = $"Atualização do seu pedido #{orderId}";
        // Simular tempo de processamento do envio de email
        await Task.Delay(100);
        
        _logger.LogInformation(
            "Email enviado para o cliente {CustomerId} sobre o pedido {OrderId}. Assunto: {Subject}",
            customerId, orderId, emailSubject);
    }

    public async Task NotifyOrderReadyAsync(Guid orderId, Guid? customerId)
    {
        if (customerId == null)
        {
            _logger.LogInformation("Pedido {OrderId} é anônimo, não será enviada notificação", orderId);
            return;
        }

        _logger.LogInformation(
            "Notificando cliente {CustomerId} que o pedido {OrderId} está pronto para retirada",
            customerId, orderId);

        string emailSubject = $"Seu pedido #{orderId} está pronto para retirada!";
        // Simular tempo de processamento do envio de email
        await Task.Delay(100);
        
        _logger.LogInformation(
            "Email de pedido pronto enviado para o cliente {CustomerId} sobre o pedido {OrderId}. Assunto: {Subject}",
            customerId, orderId, emailSubject);
    }
}

