namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string responseName = typeof(TResponse).Name;

        logger.LogInformation("[START] Handling {Request} ({@Request})", requestName, request);

        long startTime = Stopwatch.GetTimestamp();
        TResponse response = await next();
        TimeSpan delta = Stopwatch.GetElapsedTime(startTime);

        if (delta.Seconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] Long running request: {Request} ({@Request}) took {ElapsedMilliseconds} ms",
                requestName, request, delta.Seconds);
        }

        logger.LogInformation("[END] Handled {Response} ({@Response}) in {ElapsedMilliseconds} ms", responseName,
            response, delta.Seconds);
        return response;
    }
}