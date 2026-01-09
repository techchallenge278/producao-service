namespace Producao.Domain.ProductionOrders.ValueObjects;

/// <summary>
/// Item de um pedido em produção
/// </summary>
public class ProductionOrderItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    private ProductionOrderItem() 
    {
        ProductName = string.Empty;
    }

    private ProductionOrderItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static ProductionOrderItem Create(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId não pode ser vazio", nameof(productId));

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("ProductName não pode ser vazio", nameof(productName));

        if (quantity <= 0)
            throw new ArgumentException("Quantity deve ser maior que zero", nameof(quantity));

        if (unitPrice <= 0)
            throw new ArgumentException("UnitPrice deve ser maior que zero", nameof(unitPrice));

        return new ProductionOrderItem(productId, productName, quantity, unitPrice);
    }
}

