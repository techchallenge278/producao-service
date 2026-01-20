using Producao.Application.Queries;
using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;
using Moq;
using ProductionOrderItemVo = Producao.Domain.ProductionOrders.ValueObjects.ProductionOrderItem;

namespace Production.Tests.Application.Queries;

public class GetProductionOrderByOrderIdQueryHandlerTests
{
    private readonly Mock<IProductionOrderRepository> _repositoryMock;
    private readonly GetProductionOrderByOrderIdQueryHandler _handler;

    public GetProductionOrderByOrderIdQueryHandlerTests()
    {
        _repositoryMock = new Mock<IProductionOrderRepository>();
        _handler = new GetProductionOrderByOrderIdQueryHandler(
            _repositoryMock.Object,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<GetProductionOrderByOrderIdQueryHandler>.Instance);
    }

    [Fact]
    public async Task Handle_WithExistingProductionOrder_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var items = new List<ProductionOrderItemVo>
        {
            ProductionOrderItemVo.Create(Guid.NewGuid(), "Product 1", 2, 10.50m),
            ProductionOrderItemVo.Create(Guid.NewGuid(), "Product 2", 1, 15.00m)
        };

        var productionOrder = ProductionOrder.Create(orderId, customerId, "Customer Name", 36.00m, items);

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productionOrder);

        var query = new GetProductionOrderByOrderIdQuery { OrderId = orderId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Order.Should().NotBeNull();
        result.Order!.OrderId.Should().Be(orderId);
        result.Order.CustomerId.Should().Be(customerId);
        result.Order.CustomerName.Should().Be("Customer Name");
        result.Order.TotalPrice.Should().Be(36.00m);
        result.Order.Status.Should().Be(ProductionStatus.Received.ToString());
        result.Order.Items.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentOrder_ShouldReturnNotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductionOrder?)null);

        var query = new GetProductionOrderByOrderIdQuery { OrderId = orderId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Error.Should().Contain("Pedido em produção não encontrado");
        result.Order.Should().BeNull();
        _repositoryMock.Verify(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
    }
}

