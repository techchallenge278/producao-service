using Producao.Api.DTOs;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Api.DTOs;

public class UpdateProductionOrderStatusDtoTests
{
    [Fact]
    public void UpdateProductionOrderStatusDto_Properties_ShouldBeInitialized()
    {
        // Arrange & Act
        var dto = new UpdateProductionOrderStatusDto
        {
            Status = "InPreparation"
        };

        // Assert
        dto.Status.Should().Be("InPreparation");
    }
}
