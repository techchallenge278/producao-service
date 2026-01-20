using Producao.Application.Common.Exceptions;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Application.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        // Arrange
        var message = "Validation failed";

        // Act
        var exception = new ValidationException(message);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetBoth()
    {
        // Arrange
        var message = "Validation failed";
        var innerException = new InvalidOperationException("Inner error");

        // Act
        var exception = new ValidationException(message, innerException);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
    }
}
