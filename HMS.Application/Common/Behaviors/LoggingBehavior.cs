using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HMS.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        logger.LogInformation("Handling {RequestName}", requestName);
        var timer = Stopwatch.StartNew();
        
        var response = await next();
        
        timer.Stop();
        
        if (timer.ElapsedMilliseconds > 500)
        {
            logger.LogWarning("Long Running Request: {RequestName} ({ElapsedMilliseconds} ms) {@Request}", 
                requestName, timer.ElapsedMilliseconds, request);
        }
        
        logger.LogInformation("Handled {RequestName}", requestName);
        
        return response;
    }
}
