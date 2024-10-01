using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SurveyApplication.Middleware;

public class GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            _logger.LogInformation("Request received: {Path}", context.Request.Path);
            await next(context);
            _logger.LogInformation("Request completed: {Path}", context.Request.Path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var details = new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Detail = ex.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path,
            Type = "https://httpstatuses.com/500"
        };

        var response = JsonSerializer.Serialize(details);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(response);
    }
}

