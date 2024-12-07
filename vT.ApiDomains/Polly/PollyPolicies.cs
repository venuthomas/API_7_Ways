using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

public static class PollyPolicies
{
    public static AsyncRetryPolicy GetRetryPolicy(ILogger logger)
    {
        return Policy
            .Handle<Exception>() 
            .WaitAndRetryAsync(
                3,
                retryAttempt => TimeSpan.FromSeconds(2),
                (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(
                        exception,
                        "Retry {RetryCount} encountered an exception: {ExceptionType}. Waiting {Delay} before next retry.",
                        retryCount,
                        exception.GetType().Name,
                        timeSpan
                    );
                });
    }
}