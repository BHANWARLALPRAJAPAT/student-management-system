using System.Net;
using FluentValidation;
using System.Text.Json;
using StudentManagementSystem.Application.Common.Responses;

namespace StudentManagementSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationException(context, ex);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleValidationException(HttpContext context, ValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = ApiResponse<List<string>>.FailResponse(
            "Validation failed",
            400
        );

        response.Result = ex.Errors.Select(e => e.ErrorMessage).ToList();

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception occurred");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = ApiResponse<string>.FailResponse(
            "Something went wrong",
            500
        );

        response.Result = null;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}