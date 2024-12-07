using FastEndpoints;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace vT.ApiDomains.Validations;

public class ValidationBehavior<TRequest, TResponse>(
    IValidator<TRequest> validator,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IValidator<TRequest> _validator =
        validator ?? throw new ArgumentNullException(nameof(validator));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {RequestType}. Errors: {Errors}", typeof(TRequest).Name,
                validationResult.Errors);

            throw new ValidationException(validationResult.Errors);
        }

        return await next();
    }
}