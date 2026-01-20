using Producao.Application.Common.Exceptions;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Application.Common.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        // Arrange
        var message = "Resource not found";

        // Act
        var exception = new NotFoundException(message);

        // Assert
        exception.Message.Should().Be(message);
    }

    [Fact]
    public void Constructor_WithNameAndKey_ShouldFormatMessage()
    {
        // Arrange
        var name = "Order";
        var key = Guid.NewGuid();

        // Act
        var exception = new NotFoundException(name, key);

        // Assert
        exception.Message.Should().Be($"Entidade \"{name}\" ({key}) não foi encontrada.");
    }
}
