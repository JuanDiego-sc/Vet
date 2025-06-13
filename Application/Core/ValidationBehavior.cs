using FluentValidation;
using MediatR;

namespace Application.Core;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null) return await next(cancellationToken);

        var validatonResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatonResult.IsValid)
        {
            throw new ValidationException(validatonResult.Errors);
        }

        return await next(cancellationToken);
    }
}
