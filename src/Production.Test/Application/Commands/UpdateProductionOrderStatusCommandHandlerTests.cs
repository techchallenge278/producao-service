using Producao.Application.Commands;
using Producao.Application.Common.Exceptions;
using Producao.Application.Services;
using Producao.Domain.ProductionOrders.Entities;
using Producao.Domain.ProductionOrders.Repositories;
using Producao.Domain.ProductionOrders.Services;
using Producao.Domain.ProductionOrders.ValueObjects;
using FluentAssertions;
using Moq;

namespace Production.Tests.Application.Commands;

public class UpdateProductionOrderStatusCommandHandlerTests
{
    private readonly Mock<IProductionOrderRepository> _repositoryMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly Mock<IOrderServiceClient> _orderServiceClientMock;
    private readonly UpdateProductionOrderStatusCommandHandler _handler;

    public UpdateProductionOrderStatusCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProductionOrderRepository>();
        _notificationServiceMock = new Mock<INotificationService>();
        _orderServiceClientMock = new Mock<IOrderServiceClient>();
        _handler = new UpdateProductionOrderStatusCommandHandler(
            _repositoryMock.Object,
            _notificationServiceMock.Object,
            _orderServiceClientMock.Object,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<UpdateProductionOrderStatusCommandHandler>.Instance);
    }

    [Fact]
    public async Task Handle_WithValidStatus_ShouldUpdateStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var command = new UpdateProductionOrderStatusCommand
        {
            OrderId = orderId,
            Status = "InPreparation"
        };

        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var productionOrder = ProductionOrder.Create(orderId, customerId, "Cliente", 25.50m, items);

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productionOrder);

        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ProductionOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _orderServiceClientMock.Setup(x => x.UpdateOrderStatusAsync(orderId, "Processing", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be("InPreparation");
        _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ProductionOrder>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistentOrder_ShouldThrowNotFoundException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var command = new UpdateProductionOrderStatusCommand
        {
            OrderId = orderId,
            Status = "InPreparation"
        };

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductionOrder?)null);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Pedido em produção não encontrado para OrderId {orderId}");
    }

    [Fact]
    public async Task Handle_WithInvalidStatus_ShouldThrowValidationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var command = new UpdateProductionOrderStatusCommand
        {
            OrderId = orderId,
            Status = "InvalidStatus"
        };

        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var productionOrder = ProductionOrder.Create(orderId, Guid.NewGuid(), "Cliente", 25.50m, items);

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productionOrder);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_WithReadyStatus_ShouldNotifyCustomer()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var command = new UpdateProductionOrderStatusCommand
        {
            OrderId = orderId,
            Status = "Ready"
        };

        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var productionOrder = ProductionOrder.Create(orderId, customerId, "Cliente", 25.50m, items);
        productionOrder.StartPreparation();

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productionOrder);

        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ProductionOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _notificationServiceMock.Setup(x => x.NotifyOrderReadyAsync(orderId, customerId))
            .Returns(Task.CompletedTask);

        _orderServiceClientMock.Setup(x => x.UpdateOrderStatusAsync(orderId, "Ready", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be("Ready");
        _notificationServiceMock.Verify(x => x.NotifyOrderReadyAsync(orderId, customerId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithCompletedStatus_ShouldUpdateOrderStatus()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var command = new UpdateProductionOrderStatusCommand
        {
            OrderId = orderId,
            Status = "Completed"
        };

        var items = new List<ProductionOrderItem>
        {
            ProductionOrderItem.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
        };

        var productionOrder = ProductionOrder.Create(orderId, customerId, "Cliente", 25.50m, items);
        productionOrder.StartPreparation();
        productionOrder.MarkAsReady();

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(productionOrder);

        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ProductionOrder>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _orderServiceClientMock.Setup(x => x.UpdateOrderStatusAsync(orderId, "Completed", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be("Completed");
        _orderServiceClientMock.Verify(x => x.UpdateOrderStatusAsync(orderId, "Completed", It.IsAny<CancellationToken>()), Times.Once);
    }
}
