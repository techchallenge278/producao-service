using Producao.Domain.Shared.Exceptions;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Domain.Shared.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        // Arrange
        var message = "Domain error message";

        // Act
        var exception = new DomainException(message);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetBoth()
    {
        // Arrange
        var message = "Domain error message";
        var innerException = new InvalidOperationException("Inner error");

        // Act
        var exception = new DomainException(message, innerException);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
    }
}
