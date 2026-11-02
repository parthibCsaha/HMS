using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Properties containing sensitive data that should never be logged.
    /// </summary>
    private static readonly HashSet<string> SensitiveProperties = new(StringComparer.OrdinalIgnoreCase)
    {
        "Password", "PasswordHash", "CurrentPassword", "NewPassword", "ConfirmPassword",
        "Token", "RefreshToken", "AccessToken", "Secret", "ApiKey"
    };

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);
        var timer = Stopwatch.StartNew();

        var response = await next();

        timer.Stop();

        if (timer.ElapsedMilliseconds > 500)
        {
            // Log sanitized request — strip sensitive properties
            var sanitized = SanitizeRequest(request);
            logger.LogWarning(
                "Long Running Request: {RequestName} ({ElapsedMilliseconds} ms) {@Request}",
                requestName,
                timer.ElapsedMilliseconds,
                sanitized
            );
        }

        logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms",
            requestName, timer.ElapsedMilliseconds);

        return response;
    }

    /// <summary>
    /// Creates a dictionary of request properties with sensitive values masked.
    /// </summary>
    private static Dictionary<string, object?> SanitizeRequest(TRequest request)
    {
        var result = new Dictionary<string, object?>();
        var properties = typeof(TRequest).GetProperties();

        foreach (var prop in properties)
        {
            if (SensitiveProperties.Contains(prop.Name))
            {
                result[prop.Name] = "***REDACTED***";
            }
            else
            {
                result[prop.Name] = prop.GetValue(request);
            }
        }

        return result;
    }
}
