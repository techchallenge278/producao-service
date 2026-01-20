using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Domain.Shared.Entities;

public class EntityTests
{
    private class TestEntity : Producao.Domain.Shared.Entities.Entity
    {
        public TestEntity() : base() { }
    }

    [Fact]
    public void Constructor_ShouldSetIdAndCreatedAt()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        entity.Id.Should().NotBeEmpty();
        entity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void SetUpdatedAt_ShouldSetUpdatedAtToCurrentTime()
    {
        // Arrange
        var entity = new TestEntity();
        var originalUpdatedAt = entity.UpdatedAt;

        // Act
        entity.SetUpdatedAt();

        // Assert
        entity.UpdatedAt.Should().NotBeNull();
        entity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        entity.UpdatedAt.Should().NotBe(originalUpdatedAt);
    }

    [Fact]
    public void Equals_WithSameId_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Use reflection to set the same Id
        var idProperty = typeof(Producao.Domain.Shared.Entities.Entity)
            .GetProperty("Id", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        idProperty!.SetValue(entity1, id);
        idProperty.SetValue(entity2, id);

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentId_ShouldReturnFalse()
    {
        // Arrange
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Act
        var result = entity1.Equals(entity2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var result = entity.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithDifferentType_ShouldReturnFalse()
    {
        // Arrange
        var entity = new TestEntity();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };
        var productionOrder = ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "João", 25.50m, items);

        // Act
        var result = entity.Equals(productionOrder);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ShouldReturnIdHashCode()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var hashCode = entity.GetHashCode();

        // Assert
        hashCode.Should().Be(entity.Id.GetHashCode());
    }

    [Fact]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var result = entity.Equals(entity);

        // Assert
        result.Should().BeTrue();
    }
}
