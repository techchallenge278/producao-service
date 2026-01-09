using Producao.Application.Common.Exceptions;
using Producao.Application.Services;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.Services;
using Producao.Domain.ProductionOrders.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Producao.Application.Commands;

public class UpdateProductionOrderStatusCommandHandler : IRequestHandler<UpdateProductionOrderStatusCommand, UpdateProductionOrderStatusCommandResult>
{
    private readonly IProductionOrderRepository _repository;
    private readonly INotificationService _notificationService;
    private readonly IOrderServiceClient _orderServiceClient;
    private readonly ILogger<UpdateProductionOrderStatusCommandHandler> _logger;

    public UpdateProductionOrderStatusCommandHandler(
        IProductionOrderRepository repository,
        INotificationService notificationService,
        IOrderServiceClient orderServiceClient,
        ILogger<UpdateProductionOrderStatusCommandHandler> logger)
    {
        _repository = repository;
        _notificationService = notificationService;
        _orderServiceClient = orderServiceClient;
        _logger = logger;
    }

    public async Task<UpdateProductionOrderStatusCommandResult> Handle(UpdateProductionOrderStatusCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Atualizando status do pedido em produção {OrderId} para {Status}", request.OrderId, request.Status);

        var productionOrder = await _repository.GetByOrderIdAsync(request.OrderId, cancellationToken);
        if (productionOrder == null)
            throw new NotFoundException($"Pedido em produção não encontrado para OrderId {request.OrderId}");

        if (!Enum.TryParse<ProductionStatus>(request.Status, true, out var newStatus))
            throw new ValidationException($"Status inválido: {request.Status}");

        var previousStatus = productionOrder.Status.ToString();

        // Atualizar status
        productionOrder.UpdateStatus(newStatus);
        await _repository.UpdateAsync(productionOrder, cancellationToken);

        // Notificar mudança de status
        await _notificationService.NotifyOrderStatusChangeAsync(
            productionOrder.OrderId,
            previousStatus,
            newStatus.ToString(),
            productionOrder.CustomerId
        );

        // Se ficou pronto, enviar notificação especial
        if (newStatus == ProductionStatus.Ready)
        {
            _logger.LogInformation("Pedido {OrderId} está pronto! Enviando notificação especial", productionOrder.OrderId);
            await _notificationService.NotifyOrderReadyAsync(productionOrder.OrderId, productionOrder.CustomerId);
        }

        // Atualizar status no Order Service quando mudar para Ready ou Completed
        if (newStatus == ProductionStatus.Ready || newStatus == ProductionStatus.Completed)
        {
            try
            {
                var orderStatus = newStatus == ProductionStatus.Ready ? "Ready" : "Completed";
                await _orderServiceClient.UpdateOrderStatusAsync(productionOrder.OrderId, orderStatus, cancellationToken);
                _logger.LogInformation("Status do pedido {OrderId} atualizado para '{OrderStatus}' no Order Service", 
                    productionOrder.OrderId, orderStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do pedido {OrderId} no Order Service", productionOrder.OrderId);
                // Não falha a atualização de produção se não conseguir atualizar o pedido
            }
        }

        _logger.LogInformation("Status do pedido {OrderId} atualizado com sucesso para {Status}", request.OrderId, newStatus);

        return new UpdateProductionOrderStatusCommandResult
        {
            Id = productionOrder.Id,
            OrderId = productionOrder.OrderId,
            Status = productionOrder.Status.ToString(),
            UpdatedAt = productionOrder.UpdatedAt ?? DateTime.UtcNow,
            NotificationSent = true
        };
    }
}

