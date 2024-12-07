using MediatR;
using Microsoft.Extensions.Logging;
using Polly.Retry;

namespace vT.ApiDomains.Polly;

public class PollyPipelineBehavior<TRequest, TResponse>(ILogger<PollyPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<PollyPipelineBehavior<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly AsyncRetryPolicy _retryPolicy = PollyPolicies.GetRetryPolicy(logger);

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing request: {RequestName}", typeof(TRequest).Name);

        try
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await next().WaitAsync(cancellationToken);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to process request: {RequestName} after all retry attempts",
                typeof(TRequest).Name
            );

            throw;
        }
    }
}