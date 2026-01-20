using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;

namespace Production.Tests.Domain.ProductionOrders;

public class ProductionOrderTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProductionOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        // Act
        var productionOrder = ProductionOrder.Create(orderId, customerId, "João Silva", 25.50m, items);

        // Assert
        productionOrder.Should().NotBeNull();
        productionOrder.OrderId.Should().Be(orderId);
        productionOrder.CustomerId.Should().Be(customerId);
        productionOrder.Status.Should().Be(ProductionStatus.Received);
        productionOrder.Items.Should().HaveCount(1);
    }

    [Fact]
    public void Create_WithEmptyOrderId_ShouldThrowException()
    {
        // Arrange
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        // Act & Assert
        var act = () => ProductionOrder.Create(Guid.Empty, null, null, 25.50m, items);
        act.Should().Throw<ArgumentException>()
            .WithMessage("OrderId não pode ser vazio*");
    }

    [Fact]
    public void Create_WithEmptyItems_ShouldThrowException()
    {
        // Act & Assert
        var act = () => ProductionOrder.Create(Guid.NewGuid(), null, null, 25.50m, new List<ProductionOrderItem>());
        act.Should().Throw<ArgumentException>()
            .WithMessage("O pedido deve ter pelo menos um item*");
    }

    [Fact]
    public void StartPreparation_WhenStatusIsReceived_ShouldStartPreparation()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act
        productionOrder.StartPreparation();

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.InPreparation);
        productionOrder.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void StartPreparation_WhenStatusIsNotReceived_ShouldThrowException()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();

        // Act & Assert
        var act = () => productionOrder.StartPreparation();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Não é possível iniciar preparação com status {ProductionStatus.InPreparation}*");
    }

    [Fact]
    public void MarkAsReady_WhenStatusIsInPreparation_ShouldMarkAsReady()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();

        // Act
        productionOrder.MarkAsReady();

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Ready);
        productionOrder.ReadyAt.Should().NotBeNull();
    }

    [Fact]
    public void Complete_WhenStatusIsReady_ShouldComplete()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();

        // Act
        productionOrder.Complete();

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Completed);
        productionOrder.CompletedAt.Should().NotBeNull();
    }

    [Fact]
    public void GetWaitingTimeMinutes_WhenStatusIsCompleted_ShouldReturnTimeFromCompletedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();
        productionOrder.Complete();

        // Act
        var waitingTime = productionOrder.GetWaitingTimeMinutes();

        // Assert
        waitingTime.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void GetWaitingTimeMinutes_WhenStatusIsReady_ShouldReturnTimeFromReadyAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();

        // Act
        var waitingTime = productionOrder.GetWaitingTimeMinutes();

        // Assert
        waitingTime.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void GetWaitingTimeMinutes_WhenStartedAtIsSet_ShouldReturnTimeFromStartedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();

        // Act
        var waitingTime = productionOrder.GetWaitingTimeMinutes();

        // Assert
        waitingTime.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void GetWaitingTimeMinutes_WhenStatusIsReceived_ShouldReturnTimeFromCreatedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act
        var waitingTime = productionOrder.GetWaitingTimeMinutes();

        // Assert
        waitingTime.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void MarkAsReady_WhenStatusIsNotInPreparation_ShouldThrowException()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act & Assert
        var act = () => productionOrder.MarkAsReady();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Não é possível marcar como pronto com status {ProductionStatus.Received}*");
    }

    [Fact]
    public void Complete_WhenStatusIsNotReady_ShouldThrowException()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act & Assert
        var act = () => productionOrder.Complete();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Não é possível finalizar com status {ProductionStatus.Received}*");
    }

    [Fact]
    public void Cancel_WhenStatusIsNotCompleted_ShouldCancel()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act
        productionOrder.Cancel();

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Cancelled);
    }

    [Fact]
    public void Cancel_WhenStatusIsCompleted_ShouldThrowException()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();
        productionOrder.Complete();

        // Act & Assert
        var act = () => productionOrder.Cancel();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Não é possível cancelar um pedido já finalizado*");
    }

    [Fact]
    public void UpdateStatus_WithInPreparationStatus_ShouldUpdateStatusAndSetStartedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act
        productionOrder.UpdateStatus(ProductionStatus.InPreparation);

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.InPreparation);
        productionOrder.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void UpdateStatus_WithReadyStatus_ShouldUpdateStatusAndSetReadyAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();

        // Act
        productionOrder.UpdateStatus(ProductionStatus.Ready);

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Ready);
        productionOrder.ReadyAt.Should().NotBeNull();
    }

    [Fact]
    public void UpdateStatus_WithCompletedStatus_ShouldUpdateStatusAndSetCompletedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();

        // Act
        productionOrder.UpdateStatus(ProductionStatus.Completed);

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Completed);
        productionOrder.CompletedAt.Should().NotBeNull();
    }

    [Fact]
    public void UpdateStatus_WithCancelledStatus_ShouldUpdateStatus()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();

        // Act
        productionOrder.UpdateStatus(ProductionStatus.Cancelled);

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.Cancelled);
    }

    [Fact]
    public void UpdateStatus_WithInPreparationWhenAlreadyStarted_ShouldNotChangeStartedAt()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        var originalStartedAt = productionOrder.StartedAt;

        // Act
        productionOrder.UpdateStatus(ProductionStatus.InPreparation);

        // Assert
        productionOrder.Status.Should().Be(ProductionStatus.InPreparation);
        productionOrder.StartedAt.Should().Be(originalStartedAt);
    }

    [Fact]
    public void UpdateStatus_WhenStatusIsCompleted_ShouldThrowException()
    {
        // Arrange
        var productionOrder = CreateValidProductionOrder();
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();
        productionOrder.Complete();

        // Act & Assert
        var act = () => productionOrder.UpdateStatus(ProductionStatus.Cancelled);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Não é possível alterar status de um pedido finalizado*");
    }

    [Fact]
    public void Create_WithMultipleItems_ShouldCalculateEstimatedMinutes()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m),
            ProductionOrderItem.Create(Guid.NewGuid(), "Batata", 1, 15.00m),
            ProductionOrderItem.Create(Guid.NewGuid(), "Refrigerante", 1, 8.00m)
        };

        // Act
        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "João Silva", 48.50m, items);

        // Assert
        productionOrder.EstimatedMinutes.Should().BeGreaterThanOrEqualTo(10);
        productionOrder.Items.Should().HaveCount(3);
    }

    private static ProductionOrder CreateValidProductionOrder() // NOSONAR S2325
    {
        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };
        return ProductionOrder.Create(Guid.NewGuid(), Guid.NewGuid(), "João Silva", 25.50m, items);
    }
}

