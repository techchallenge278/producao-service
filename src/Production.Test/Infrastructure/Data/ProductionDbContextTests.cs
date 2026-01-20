using Producao.Infrastructure.Data;
using FluentAssertions;
using Moq;
using MongoDB.Driver;

namespace Production.Tests.Infrastructure.Data;

public class ProductionDbContextTests
{
    [Fact]
    public void Constructor_ShouldInitialize()
    {
        // Arrange
        var databaseMock = new Mock<IMongoDatabase>();

        // Act
        var context = new ProductionDbContext(databaseMock.Object);

        // Assert
        context.Should().NotBeNull();
    }

    [Fact]
    public void ProductionOrders_ShouldReturnCollection()
    {
        // Arrange
        var databaseMock = new Mock<IMongoDatabase>();
        var collectionMock = new Mock<IMongoCollection<Producao.Domain.ProductionOrders.Entities.ProductionOrder>>();

        databaseMock
            .Setup(x => x.GetCollection<Producao.Domain.ProductionOrders.Entities.ProductionOrder>(
                It.IsAny<string>(), 
                It.IsAny<MongoCollectionSettings>()))
            .Returns(collectionMock.Object);

        var context = new ProductionDbContext(databaseMock.Object);

        // Act
        var collection = context.ProductionOrders;

        // Assert
        collection.Should().NotBeNull();
    }
}

