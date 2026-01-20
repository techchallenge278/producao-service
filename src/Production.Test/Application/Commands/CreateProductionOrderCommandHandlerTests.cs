using Producao.Application.Commands;
using Producao.Application.Common.Exceptions;
using Producao.Domain.ProductionOrders.Repositories;
using ProductionOrderEntity = Producao.Domain.ProductionOrders.Entities.ProductionOrder;
using ProductionOrderItemVo = Producao.Domain.ProductionOrders.ValueObjects.ProductionOrderItem;
using FluentAssertions;
using Moq;

namespace Production.Tests.Application.Commands;

public class CreateProductionOrderCommandHandlerTests
{
    private readonly Mock<IProductionOrderRepository> _repositoryMock;
    private readonly CreateProductionOrderCommandHandler _handler;

    public CreateProductionOrderCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProductionOrderRepository>();
        _handler = new CreateProductionOrderCommandHandler(
            _repositoryMock.Object,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<CreateProductionOrderCommandHandler>.Instance);
    }

    [Fact]
    public async Task Handle_WithValidData_ShouldCreateProductionOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var command = new CreateProductionOrderCommand
        {
            OrderId = orderId,
            CustomerId = Guid.NewGuid(),
            CustomerName = "João Silva",
            TotalPrice = 25.50m,
            Items = new List<CreateProductionOrderItemCommand>
            {
                new CreateProductionOrderItemCommand
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Hambúrguer",
                    Quantity = 1,
                    UnitPrice = 25.50m
                }
            }
        };

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductionOrderEntity?)null);

        _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<ProductionOrderEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductionOrderEntity order, CancellationToken ct) => order);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(orderId);
        result.Status.Should().Be("Received");
        _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<ProductionOrderEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingProductionOrder_ShouldThrowValidationException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var command = new CreateProductionOrderCommand
        {
            OrderId = orderId,
            TotalPrice = 25.50m,
            Items = new List<CreateProductionOrderItemCommand>
            {
                new CreateProductionOrderItemCommand
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Hambúrguer",
                    Quantity = 1,
                    UnitPrice = 25.50m
                }
            }
        };

        var existingOrder = ProductionOrderEntity.Create(
            orderId,
            null,
            null,
            25.50m,
            new List<ProductionOrderItemVo>
            {
                ProductionOrderItemVo.Create(Guid.NewGuid(), "Hambúrguer", 1, 25.50m)
            });

        _repositoryMock.Setup(x => x.GetByOrderIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingOrder);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage($"Já existe um pedido em produção para o OrderId {orderId}");
    }
}

