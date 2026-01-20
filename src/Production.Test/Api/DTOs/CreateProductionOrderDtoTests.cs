using Producao.Api.DTOs;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Production.Tests.Api.DTOs;

public class CreateProductionOrderDtoTests
{
    [Fact]
    public void CreateProductionOrderDto_Properties_ShouldBeInitialized()
    {
        // Arrange & Act
        var dto = new CreateProductionOrderDto
        {
            OrderId = Guid.NewGuid(),
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

        // Assert
        dto.OrderId.Should().NotBeEmpty();
        dto.CustomerId.Should().NotBeNull();
        dto.CustomerName.Should().Be("João Silva");
        dto.TotalPrice.Should().Be(100.50m);
        dto.Items.Should().HaveCount(1);
    }

    [Fact]
    public void CreateProductionOrderItemDto_Properties_ShouldBeInitialized()
    {
        // Arrange & Act
        var itemDto = new CreateProductionOrderItemDto
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Hambúrguer",
            Quantity = 2,
            UnitPrice = 25.50m
        };

        // Assert
        itemDto.ProductId.Should().NotBeEmpty();
        itemDto.ProductName.Should().Be("Hambúrguer");
        itemDto.Quantity.Should().Be(2);
        itemDto.UnitPrice.Should().Be(25.50m);
    }
}
