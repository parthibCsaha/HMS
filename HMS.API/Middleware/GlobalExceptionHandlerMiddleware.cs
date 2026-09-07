using System.Net;
using System.Text.Json;
using HMS.Application.Common.Exceptions;
using HMS.Application.Common.Models;

namespace HMS.API.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, response) = exception switch
        {
            ValidationException validationEx => (
                HttpStatusCode.UnprocessableEntity,
                ApiResponse.Fail(validationEx.Message, validationEx.Errors)),

            NotFoundException notFoundEx => (
                HttpStatusCode.NotFound,
                ApiResponse.Fail(notFoundEx.Message)),

            BadRequestException badRequestEx => (
                HttpStatusCode.BadRequest,
                ApiResponse.Fail(badRequestEx.Message)),

            ConflictException conflictEx => (
                HttpStatusCode.Conflict,
                ApiResponse.Fail(conflictEx.Message)),

            UnauthorizedException unauthorizedEx => (
                HttpStatusCode.Unauthorized,
                ApiResponse.Fail(unauthorizedEx.Message)),

            ForbiddenException forbiddenEx => (
                HttpStatusCode.Forbidden,
                ApiResponse.Fail(forbiddenEx.Message)),

            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                ApiResponse.Fail("You are not authorized to perform this action.")),

            _ => (
                HttpStatusCode.InternalServerError,
                ApiResponse.Fail("An unexpected error occurred. Please try again later."))
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        }
        else
        {
            logger.LogWarning("Handled exception [{StatusCode}]: {Message}", (int)statusCode, exception.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(json);
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
