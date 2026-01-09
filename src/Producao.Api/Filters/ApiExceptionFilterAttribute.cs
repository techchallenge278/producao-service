using Producao.Application.Common.Exceptions;
using Producao.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Producao.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] // NOSONAR S3993
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private static void HandleException(ExceptionContext context) // NOSONAR S2325
    {
        var exception = context.Exception;
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case NotFoundException notFoundException:
                HandleNotFoundException(context, notFoundException, problemDetails);
                break;

            case ValidationException validationException:
                HandleValidationException(context, validationException, problemDetails);
                break;

            case DomainException domainException:
                HandleDomainException(context, domainException, problemDetails);
                break;

            default:
                HandleUnknownException(context, exception, problemDetails);
                break;
        }

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context, NotFoundException exception, ProblemDetails problemDetails) // NOSONAR S2325
    {
        problemDetails.Status = StatusCodes.Status404NotFound;
        problemDetails.Title = exception.Message;
        problemDetails.Type = "NotFound";
        problemDetails.Detail = exception.Message;
        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
    }

    private static void HandleValidationException(ExceptionContext context, ValidationException exception, ProblemDetails problemDetails) // NOSONAR S2325
    {
        problemDetails.Status = StatusCodes.Status400BadRequest;
        problemDetails.Title = exception.Message;
        problemDetails.Type = "ValidationFailure";
        problemDetails.Detail = exception.Message;
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }

    private static void HandleDomainException(ExceptionContext context, DomainException exception, ProblemDetails problemDetails) // NOSONAR S2325
    {
        problemDetails.Status = StatusCodes.Status400BadRequest;
        problemDetails.Title = exception.Message;
        problemDetails.Type = "DomainError";
        problemDetails.Detail = exception.Message;
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }

    private static void HandleUnknownException(ExceptionContext context, Exception exception, ProblemDetails problemDetails) // NOSONAR S2325
    {
        problemDetails.Status = StatusCodes.Status500InternalServerError;
        problemDetails.Title = "Um erro ocorreu ao processar sua requisição.";
        problemDetails.Type = "ServerError";
        problemDetails.Detail = exception.Message;
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }
}

