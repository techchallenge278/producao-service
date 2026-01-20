using Producao.Api.Filters;
using Producao.Application.Common.Exceptions;
using Producao.Domain.Shared.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace Production.Tests.Api.Filters;

public class ApiExceptionFilterAttributeTests
{
    [Fact]
    public void OnException_WithNotFoundException_ShouldReturnNotFoundResult()
    {
        // Arrange
        var filter = new ApiExceptionFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new NotFoundException("Resource not found")
        };

        // Act
        filter.OnException(exceptionContext);

        // Assert
        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<ObjectResult>();
        var objectResult = exceptionContext.Result as ObjectResult;
        objectResult!.StatusCode.Should().Be(404);
        objectResult.Value.Should().BeOfType<ProblemDetails>();
        var problemDetails = objectResult.Value as ProblemDetails;
        problemDetails!.Status.Should().Be(404);
        problemDetails.Title.Should().Be("Resource not found");
        problemDetails.Type.Should().Be("NotFound");
        problemDetails.Detail.Should().Be("Resource not found");
        httpContext.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public void OnException_WithValidationException_ShouldReturnBadRequestResult()
    {
        // Arrange
        var filter = new ApiExceptionFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new ValidationException("Validation failed")
        };

        // Act
        filter.OnException(exceptionContext);

        // Assert
        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<ObjectResult>();
        var objectResult = exceptionContext.Result as ObjectResult;
        objectResult!.StatusCode.Should().Be(400);
        objectResult.Value.Should().BeOfType<ProblemDetails>();
        var problemDetails = objectResult.Value as ProblemDetails;
        problemDetails!.Status.Should().Be(400);
        problemDetails.Title.Should().Be("Validation failed");
        problemDetails.Type.Should().Be("ValidationFailure");
        problemDetails.Detail.Should().Be("Validation failed");
        httpContext.Response.StatusCode.Should().Be(400);
    }

    [Fact]
    public void OnException_WithDomainException_ShouldReturnBadRequestResult()
    {
        // Arrange
        var filter = new ApiExceptionFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new DomainException("Domain error occurred")
        };

        // Act
        filter.OnException(exceptionContext);

        // Assert
        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<ObjectResult>();
        var objectResult = exceptionContext.Result as ObjectResult;
        objectResult!.StatusCode.Should().Be(400);
        objectResult.Value.Should().BeOfType<ProblemDetails>();
        var problemDetails = objectResult.Value as ProblemDetails;
        problemDetails!.Status.Should().Be(400);
        problemDetails.Title.Should().Be("Domain error occurred");
        problemDetails.Type.Should().Be("DomainError");
        problemDetails.Detail.Should().Be("Domain error occurred");
        httpContext.Response.StatusCode.Should().Be(400);
    }

    [Fact]
    public void OnException_WithUnknownException_ShouldReturnInternalServerErrorResult()
    {
        // Arrange
        var filter = new ApiExceptionFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InvalidOperationException("Something went wrong")
        };

        // Act
        filter.OnException(exceptionContext);

        // Assert
        exceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionContext.Result.Should().BeOfType<ObjectResult>();
        var objectResult = exceptionContext.Result as ObjectResult;
        objectResult!.StatusCode.Should().Be(500);
        objectResult.Value.Should().BeOfType<ProblemDetails>();
        var problemDetails = objectResult.Value as ProblemDetails;
        problemDetails!.Status.Should().Be(500);
        problemDetails.Title.Should().Be("Um erro ocorreu ao processar sua requisição.");
        problemDetails.Type.Should().Be("ServerError");
        problemDetails.Detail.Should().Be("Something went wrong");
        httpContext.Response.StatusCode.Should().Be(500);
    }
}
