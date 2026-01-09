using Producao.Application.Common.Exceptions;
using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Producao.Application.Commands;

public class CreateProductionOrderCommandHandler : IRequestHandler<CreateProductionOrderCommand, CreateProductionOrderCommandResult>
{
    private readonly IProductionOrderRepository _repository;
    private readonly ILogger<CreateProductionOrderCommandHandler> _logger;

    public CreateProductionOrderCommandHandler(
        IProductionOrderRepository repository,
        ILogger<CreateProductionOrderCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CreateProductionOrderCommandResult> Handle(CreateProductionOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Criando pedido em produção para OrderId {OrderId}", request.OrderId);

        // Verificar se já existe um pedido em produção para este OrderId
        var existing = await _repository.GetByOrderIdAsync(request.OrderId, cancellationToken);
        if (existing != null)
        {
            _logger.LogWarning("Já existe um pedido em produção para OrderId {OrderId}", request.OrderId);
            throw new ValidationException($"Já existe um pedido em produção para o OrderId {request.OrderId}");
        }

        // Criar itens
        var items = request.Items.Select(item => 
            ProductionOrderItem.Create(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice)
        ).ToList();

        // Criar pedido em produção
        var productionOrder = ProductionOrder.Create(
            request.OrderId,
            request.CustomerId,
            request.CustomerName,
            request.TotalPrice,
            items
        );

        // Salvar
        await _repository.CreateAsync(productionOrder, cancellationToken);

        _logger.LogInformation("Pedido em produção criado com sucesso. Id: {Id}, OrderId: {OrderId}", 
            productionOrder.Id, request.OrderId);

        return new CreateProductionOrderCommandResult
        {
            Id = productionOrder.Id,
            OrderId = productionOrder.OrderId,
            Status = productionOrder.Status.ToString(),
            EstimatedMinutes = productionOrder.EstimatedMinutes,
            CreatedAt = productionOrder.CreatedAt
        };
    }
}

