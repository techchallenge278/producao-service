using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;

namespace Production.Tests.Domain.ProductionOrders;

public class ProductionOrderItemTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProductionOrderItem()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productName = "Hambúrguer";
        var quantity = 2;
        var unitPrice = 25.50m;

        // Act
        var item = ProductionOrderItem.Create(productId, productName, quantity, unitPrice);

        // Assert
        item.Should().NotBeNull();
        item.ProductId.Should().Be(productId);
        item.ProductName.Should().Be(productName);
        item.Quantity.Should().Be(quantity);
        item.UnitPrice.Should().Be(unitPrice);
    }

    [Fact]
    public void Create_WithEmptyProductId_ShouldThrowException()
    {
        // Act & Assert
        var act = () => ProductionOrderItem.Create(Guid.Empty, "Hambúrguer", 1, 25.50m);
        act.Should().Throw<ArgumentException>()
            .WithMessage("ProductId não pode ser vazio*");
    }

    [Fact]
    public void Create_WithEmptyProductName_ShouldThrowException()
    {
        // Act & Assert
        var act = () => ProductionOrderItem.Create(Guid.NewGuid(), "", 1, 25.50m);
        act.Should().Throw<ArgumentException>()
            .WithMessage("ProductName não pode ser vazio*");
    }

    [Fact]
    public void Create_WithZeroQuantity_ShouldThrowException()
    {
        // Act & Assert
        var act = () => ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 0, 25.50m);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Quantity deve ser maior que zero*");
    }

    [Fact]
    public void Create_WithZeroUnitPrice_ShouldThrowException()
    {
        // Act & Assert
        var act = () => ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 0m);
        act.Should().Throw<ArgumentException>()
            .WithMessage("UnitPrice deve ser maior que zero*");
    }
}

