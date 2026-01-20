using Producao.Api.Models;
using FluentAssertions;
using Xunit;

namespace Production.Tests.Api.Models;

public class ErrorResponseTests
{
    [Fact]
    public void Constructor_Default_ShouldInitializeErrorsList()
    {
        // Arrange & Act
        var response = new ErrorResponse();

        // Assert
        response.Should().NotBeNull();
        response.Errors.Should().NotBeNull().And.BeEmpty();
        response.TraceId.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithSingleError_ShouldAddErrorToList()
    {
        // Arrange
        var errorMessage = "Single error message.";

        // Act
        var response = new ErrorResponse(errorMessage);

        // Assert
        response.Should().NotBeNull();
        response.Errors.Should().ContainSingle(errorMessage);
        response.TraceId.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithMultipleErrors_ShouldAddAllErrorsToList()
    {
        // Arrange
        var errorMessages = new List<string> { "Error 1.", "Error 2." };

        // Act
        var response = new ErrorResponse(errorMessages);

        // Assert
        response.Should().NotBeNull();
        response.Errors.Should().HaveCount(2).And.Contain(errorMessages);
        response.TraceId.Should().BeNull();
    }

    [Fact]
    public void Properties_ShouldBeSettable()
    {
        // Arrange
        var response = new ErrorResponse();
        var newErrors = new List<string> { "New error." };
        var newTraceId = "trace-123";

        // Act
        response.Errors = newErrors;
        response.TraceId = newTraceId;

        // Assert
        response.Errors.Should().BeSameAs(newErrors);
        response.TraceId.Should().Be(newTraceId);
    }
}
