using Producao.Application.Queries;
using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;
using Moq;
using ProductionOrderItemVo = Producao.Domain.ProductionOrders.ValueObjects.ProductionOrderItem;

namespace Production.Tests.Application.Queries;

public class GetOrdersInProductionQueryHandlerTests
{
    private readonly Mock<IProductionOrderRepository> _repositoryMock;
    private readonly GetOrdersInProductionQueryHandler _handler;

    public GetOrdersInProductionQueryHandlerTests()
    {
        _repositoryMock = new Mock<IProductionOrderRepository>();
        _handler = new GetOrdersInProductionQueryHandler(
            _repositoryMock.Object,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<GetOrdersInProductionQueryHandler>.Instance);
    }

    [Fact]
    public async Task Handle_WithValidStatus_ShouldReturnFilteredOrders()
    {
        // Arrange
        var query = new GetOrdersInProductionQuery
        {
            Status = "InPreparation",
            PageNumber = 1,
            PageSize = 10
        };

        var items = new List<ProductionOrderItemVo>
        {
            ProductionOrderItemVo.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var orders = new List<ProductionOrder>
        {
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 1", 25.50m, items)
        };

        orders[0].StartPreparation();

        _repositoryMock.Setup(x => x.GetByStatusAsync(
            ProductionStatus.InPreparation, 
            query.PageNumber, 
            query.PageSize, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Orders.Should().HaveCount(1);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalCount.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithNullStatus_ShouldReturnAllOrdersInProduction()
    {
        // Arrange
        var query = new GetOrdersInProductionQuery
        {
            Status = null,
            PageNumber = 1,
            PageSize = 10
        };

        var items = new List<ProductionOrderItemVo>
        {
            ProductionOrderItemVo.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var orders = new List<ProductionOrder>
        {
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 1", 25.50m, items),
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 2", 30.00m, items)
        };

        _repositoryMock.Setup(x => x.GetOrdersInProductionAsync(
            query.PageNumber, 
            query.PageSize, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Orders.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.GetOrdersInProductionAsync(
            query.PageNumber, 
            query.PageSize, 
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidStatus_ShouldReturnAllOrdersInProduction()
    {
        // Arrange
        var query = new GetOrdersInProductionQuery
        {
            Status = "InvalidStatus",
            PageNumber = 1,
            PageSize = 10
        };

        var items = new List<ProductionOrderItemVo>
        {
            ProductionOrderItemVo.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var orders = new List<ProductionOrder>
        {
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 1", 25.50m, items)
        };

        _repositoryMock.Setup(x => x.GetOrdersInProductionAsync(
            query.PageNumber, 
            query.PageSize, 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Orders.Should().HaveCount(1);
        _repositoryMock.Verify(x => x.GetOrdersInProductionAsync(
            query.PageNumber, 
            query.PageSize, 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
