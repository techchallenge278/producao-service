using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.ValueObjects;
using Producao.Infrastructure.Data;
using Producao.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using MongoDB.Driver;

namespace Production.Tests.Infrastructure.Repositories;

public class ProductionOrderRepositoryTests
{
    private readonly Mock<IMongoCollection<ProductionOrder>> _collectionMock;
    private readonly Mock<IMongoDatabase> _databaseMock;
    private readonly ProductionOrderRepository _repository;

    public ProductionOrderRepositoryTests()
    {
        _collectionMock = new Mock<IMongoCollection<ProductionOrder>>();
        _databaseMock = new Mock<IMongoDatabase>();
        _databaseMock
            .Setup(x => x.GetCollection<ProductionOrder>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
            .Returns(_collectionMock.Object);

        var context = new ProductionDbContext(_databaseMock.Object);
        _repository = new ProductionOrderRepository(context);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingOrder_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Product 1", 1, 10.00m)
        };
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Customer", 10.00m, items);

        var mockCursor = new Mock<IAsyncCursor<ProductionOrder>>();
        mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        mockCursor.Setup(x => x.Current).Returns(new[] { productionOrder });

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetByIdAsync(productionOrder.Id);

        // Assert
        result.Should().NotBeNull();
        result!.OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Product 1", 1, 10.00m)
        };
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Customer", 10.00m, items);

        _collectionMock.Setup(x => x.InsertOneAsync(
            It.IsAny<ProductionOrder>(),
            It.IsAny<InsertOneOptions>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _repository.CreateAsync(productionOrder);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(productionOrder.Id);
        _collectionMock.Verify(x => x.InsertOneAsync(
            productionOrder,
            It.IsAny<InsertOneOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Product 1", 1, 10.00m)
        };
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Customer", 10.00m, items);

        var replaceResult = new Mock<ReplaceOneResult>();
        replaceResult.Setup(x => x.IsAcknowledged).Returns(true);

        _collectionMock.Setup(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<ProductionOrder>>(),
            It.IsAny<ProductionOrder>(),
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(replaceResult.Object);

        // Act
        await _repository.UpdateAsync(productionOrder);

        // Assert
        _collectionMock.Verify(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<ProductionOrder>>(),
            productionOrder,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Product 1", 1, 10.00m)
        };
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Customer", 10.00m, items);

        var deleteResult = new Mock<DeleteResult>();
        deleteResult.Setup(x => x.IsAcknowledged).Returns(true);

        _collectionMock.Setup(x => x.DeleteOneAsync(
            It.IsAny<FilterDefinition<ProductionOrder>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(deleteResult.Object);

        // Act
        await _repository.DeleteAsync(productionOrder);

        // Assert
        _collectionMock.Verify(x => x.DeleteOneAsync(
            It.IsAny<FilterDefinition<ProductionOrder>>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByOrderIdAsync_WithExistingOrder_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Product 1", 1, 10.00m)
        };
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Customer", 10.00m, items);

        var mockCursor = new Mock<IAsyncCursor<ProductionOrder>>();
        mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        mockCursor.Setup(x => x.Current).Returns(new[] { productionOrder });

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _repository.GetByOrderIdAsync(orderId);

        // Assert
        result.Should().NotBeNull();
        result!.OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPagedOrders()
    {
        // Arrange
        var orders = new List<ProductionOrder>
        {
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 1", 10m,
                new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 10m) }),
            ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 2", 20m,
                new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 20m) })
        };

        var cursor = new Mock<IAsyncCursor<ProductionOrder>>();
        cursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        cursor.SetupGet(x => x.Current).Returns(orders);

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursor.Object);

        // Act
        var result = await _repository.GetAllAsync(1, 10, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetByStatusAsync_ShouldReturnOrders()
    {
        // Arrange
        var order = ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente", 10m,
            new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 10m) });
        order.StartPreparation();

        var orders = new List<ProductionOrder> { order };

        var cursor = new Mock<IAsyncCursor<ProductionOrder>>();
        cursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        cursor.SetupGet(x => x.Current).Returns(orders);

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursor.Object);

        // Act
        var result = await _repository.GetByStatusAsync(ProductionStatus.InPreparation, 1, 10, CancellationToken.None);

        // Assert
        result.Should().ContainSingle();
        result.First().Status.Should().Be(ProductionStatus.InPreparation);
    }

    [Fact]
    public async Task GetOrdersInProductionAsync_ShouldReturnOrdersInProduction()
    {
        // Arrange
        var order1 = ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 1", 10m,
            new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 10m) });
        var order2 = ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente 2", 20m,
            new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 20m) });
        order2.StartPreparation();

        var orders = new List<ProductionOrder> { order1, order2 };

        var cursor = new Mock<IAsyncCursor<ProductionOrder>>();
        cursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        cursor.SetupGet(x => x.Current).Returns(orders);

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursor.Object);

        // Act
        var result = await _repository.GetOrdersInProductionAsync(1, 10, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetReadyOrdersAsync_ShouldReturnReadyOrders()
    {
        // Arrange
        var order = ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "Cliente", 10m,
            new List<ProductionOrderItem> { ProductionOrderItem.Create(Guid.NewGuid(), "Produto", 1, 10m) });
        order.StartPreparation();
        order.MarkAsReady();

        var orders = new List<ProductionOrder> { order };

        var cursor = new Mock<IAsyncCursor<ProductionOrder>>();
        cursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        cursor.SetupGet(x => x.Current).Returns(orders);

        _collectionMock.Setup(x => x.FindAsync(
                It.IsAny<FilterDefinition<ProductionOrder>>(),
                It.IsAny<FindOptions<ProductionOrder, ProductionOrder>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursor.Object);

        // Act
        var result = await _repository.GetReadyOrdersAsync(1, 10, CancellationToken.None);

        // Assert
        result.Should().ContainSingle();
        result.First().Status.Should().Be(ProductionStatus.Ready);
    }
}

