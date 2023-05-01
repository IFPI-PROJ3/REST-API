using Proj3.Application.Common.Errors;
using System.Net;
using System.Text.Json;

namespace Proj3.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleUnauthorizedAccessException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleUnauthorizedAccessException(HttpContext context, Exception exception)
    {
        HttpStatusCode code = HttpStatusCode.Unauthorized;

        string? result = JsonSerializer.Serialize(new
        {
            title = "Unauthorized Access",
            type = "",
            detail = exception.Message,
            status = (int)code,
            traceId = context.TraceIdentifier
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if(exception is IExceptionBase)
        {
            IExceptionBase exceptionBase = (IExceptionBase)exception;

            string? result = JsonSerializer.Serialize(new
            {
                title = "Invalid request",
                type = nameof(exception),
                detail = exceptionBase.ErrorMessage,
                status = exceptionBase.StatusCode,
                traceId = context.TraceIdentifier
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exceptionBase.StatusCode;

            return context.Response.WriteAsync(result);
        }
        else
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            string? result = JsonSerializer.Serialize(new
            {
                title = "An error occurred.",
                type = "Internal server error.",
                detail = string.Empty,
                status = (int)code,
                traceId = context.TraceIdentifier
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }        
    }
}