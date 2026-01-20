using Producao.Api.Controllers;
using Producao.Api.DTOs;
using Producao.Api.Models;
using Producao.Application.Commands;
using Producao.Application.Common.Exceptions;
using Producao.Application.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductionOrderItem = Producao.Application.Queries.ProductionOrderItem;
using Xunit;

namespace Production.Tests.Api.Controllers;

public class ProductionControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<ProductionController>> _loggerMock;
    private readonly ProductionController _controller;

    public ProductionControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ProductionController>>();
        _controller = new ProductionController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateProductionOrder_WithValidRequest_ShouldReturnCreatedResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var commandResult = new CreateProductionOrderCommandResult
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Status = "Received"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateProductionOrderCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        var request = new CreateProductionOrderDto
        {
            OrderId = orderId,
            CustomerId = Guid.NewGuid(),
            CustomerName = "João Silva",
            TotalPrice = 100.50m,
            Items = new List<CreateProductionOrderItemDto>
            {
                new CreateProductionOrderItemDto
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Hambúrguer",
                    Quantity = 2,
                    UnitPrice = 25.50m
                }
            }
        };

        // Act
        var result = await _controller.CreateProductionOrder(request);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtResult = result as CreatedAtActionResult;
        createdAtResult!.ActionName.Should().Be(nameof(_controller.GetByOrderId));
        createdAtResult.RouteValues!["orderId"].Should().Be(orderId);
        createdAtResult.Value.Should().Be(commandResult);
    }

    [Fact]
    public async Task GetOrdersInProduction_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var queryResult = new GetOrdersInProductionQueryResult
        {
            Orders = new List<ProductionOrderItem>(),
            TotalCount = 0,
            PageNumber = 1,
            PageSize = 10
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetOrdersInProductionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _controller.GetOrdersInProduction(null, 1, 10);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(queryResult);
    }

    [Fact]
    public async Task GetByOrderId_WithExistingOrder_ShouldReturnOkResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var queryResult = new GetProductionOrderByOrderIdQueryResult
        {
            Success = true,
            Order = new ProductionOrderDto
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                Status = "Received"
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProductionOrderByOrderIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _controller.GetByOrderId(orderId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(queryResult.Order);
    }

    [Fact]
    public async Task GetByOrderId_WithNonExistentOrder_ShouldReturnNotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var queryResult = new GetProductionOrderByOrderIdQueryResult
        {
            Success = false,
            Error = "Order not found"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProductionOrderByOrderIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _controller.GetByOrderId(orderId);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFound = result as NotFoundObjectResult;
        notFound!.Value.Should().BeOfType<ErrorResponse>();
    }

    [Fact]
    public async Task UpdateStatus_WithValidRequest_ShouldReturnOkResult()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var commandResult = new UpdateProductionOrderStatusCommandResult
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Status = "InPreparation"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProductionOrderStatusCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        var request = new UpdateProductionOrderStatusDto
        {
            Status = "InPreparation"
        };

        // Act
        var result = await _controller.UpdateStatus(orderId, request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult!.Value.Should().Be(commandResult);
    }

    [Fact]
    public async Task UpdateStatus_WithNotFoundException_ShouldReturnNotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var request = new UpdateProductionOrderStatusDto
        {
            Status = "InPreparation"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProductionOrderStatusCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException("Order not found"));

        // Act
        var result = await _controller.UpdateStatus(orderId, request);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        var notFound = result as NotFoundObjectResult;
        notFound!.Value.Should().BeOfType<ErrorResponse>();
    }

    [Fact]
    public async Task UpdateStatus_WithValidationException_ShouldReturnBadRequest()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var request = new UpdateProductionOrderStatusDto
        {
            Status = "InvalidStatus"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProductionOrderStatusCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ValidationException("Invalid status"));

        // Act
        var result = await _controller.UpdateStatus(orderId, request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var badRequest = result as BadRequestObjectResult;
        badRequest!.Value.Should().BeOfType<ErrorResponse>();
    }

    [Fact]
    public async Task UpdateStatus_WithGenericException_ShouldReturnStatusCode500()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var request = new UpdateProductionOrderStatusDto
        {
            Status = "InPreparation"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProductionOrderStatusCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Internal error"));

        // Act
        var result = await _controller.UpdateStatus(orderId, request);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(500);
        objectResult.Value.Should().BeOfType<ErrorResponse>();
    }
}
