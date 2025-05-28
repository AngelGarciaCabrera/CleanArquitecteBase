using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;


public class ValidationBehaviors<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validador;

public ValidationBehaviors(IValidator<TRequest>? validador= null)
    {
        _validador = validador;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(_validador is null) //validamos si el comando no tiene validador
        {
            return await next();
        }

        var validatorResult = await _validador.ValidateAsync(request, cancellationToken); //si tiene validador, lo ejecutamos

        if(validatorResult.IsValid)
        {
            return await next();
        }
        var errors = validatorResult.Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));
            return (dynamic)errors;        

    }
}