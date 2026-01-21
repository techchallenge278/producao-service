using Producao.Domain.ProductionOrders.ValueObjects;
using Producao.Domain.Shared.Entities;

namespace Producao.Domain.ProductionOrders.Entities;

/// <summary>
/// Entidade que representa um pedido em produção (visão da cozinha)
/// Armazenada no MongoDB
/// </summary>
public class ProductionOrder : Entity
{
    public Guid OrderId { get; private set; }
    public Guid? CustomerId { get; private set; }
    public string? CustomerName { get; private set; }
    public decimal TotalPrice { get; private set; }
    public ProductionStatus Status { get; private set; }
    public List<ProductionOrderItem> Items { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? ReadyAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public int EstimatedMinutes { get; private set; }

    private ProductionOrder() 
    { 
        Items = new List<ProductionOrderItem>();
    }

    private ProductionOrder(
        Guid orderId,
        Guid? customerId,
        string? customerName,
        decimal totalPrice,
        List<ProductionOrderItem> items)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        CustomerId = customerId;
        CustomerName = customerName;
        TotalPrice = totalPrice;
        Items = items ?? new List<ProductionOrderItem>();
        Status = ProductionStatus.Received;
        CreatedAt = DateTime.UtcNow;
        EstimatedMinutes = CalculateEstimatedMinutes(Items);
    }

    public static ProductionOrder Create(
        Guid orderId,
        Guid? customerId,
        string? customerName,
        decimal totalPrice,
        List<ProductionOrderItem> items)
    {
        if (orderId == Guid.Empty)
            throw new ArgumentException("OrderId não pode ser vazio", nameof(orderId));

        if (items == null || items.Count == 0)
            throw new ArgumentException("O pedido deve ter pelo menos um item", nameof(items));

        return new ProductionOrder(orderId, customerId, customerName, totalPrice, items);
    }

    public void StartPreparation()
    {
        if (Status != ProductionStatus.Received)
            throw new InvalidOperationException($"Não é possível iniciar preparação com status {Status}");

        Status = ProductionStatus.InPreparation;
        StartedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void MarkAsReady()
    {
        if (Status != ProductionStatus.InPreparation)
            throw new InvalidOperationException($"Não é possível marcar como pronto com status {Status}");

        Status = ProductionStatus.Ready;
        ReadyAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void Complete()
    {
        if (Status != ProductionStatus.Ready)
            throw new InvalidOperationException($"Não é possível finalizar com status {Status}");

        Status = ProductionStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void Cancel()
    {
        if (Status == ProductionStatus.Completed)
            throw new InvalidOperationException("Não é possível cancelar um pedido já finalizado");

        Status = ProductionStatus.Cancelled;
        SetUpdatedAt();
    }

    public void UpdateStatus(ProductionStatus newStatus)
    {
        if (Status == ProductionStatus.Completed)
            throw new InvalidOperationException("Não é possível alterar status de um pedido finalizado");

        Status = newStatus;
        SetUpdatedAt();

        if (newStatus == ProductionStatus.InPreparation && StartedAt == null)
            StartedAt = DateTime.UtcNow;
        else if (newStatus == ProductionStatus.Ready && ReadyAt == null)
            ReadyAt = DateTime.UtcNow;
        else if (newStatus == ProductionStatus.Completed && CompletedAt == null)
            CompletedAt = DateTime.UtcNow;
    }

    private static int CalculateEstimatedMinutes(List<ProductionOrderItem> items) 
    {
        if (items == null || items.Count == 0)
            return 10; // Tempo mínimo padrão

        // Estimativa simples: 5 minutos por item, mínimo 10 minutos
        var baseMinutes = items.Count * 5;
        return Math.Max(baseMinutes, 10);
    }

    public int GetWaitingTimeMinutes()
    {
        if (Status == ProductionStatus.Completed && CompletedAt.HasValue)
        {
            return (int)(CompletedAt.Value - CreatedAt).TotalMinutes;
        }
        else if (Status == ProductionStatus.Ready && ReadyAt.HasValue)
        {
            return (int)(ReadyAt.Value - CreatedAt).TotalMinutes;
        }
        else if (StartedAt.HasValue)
        {
            return (int)(DateTime.UtcNow - StartedAt.Value).TotalMinutes;
        }
        else
        {
            return (int)(DateTime.UtcNow - CreatedAt).TotalMinutes;
        }
    }
}

