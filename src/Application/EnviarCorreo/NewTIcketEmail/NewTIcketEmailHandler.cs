using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;

namespace Application.EnviarCorreo.NewTIcketEmail;

using Domain.DomainErros;


public sealed class NewTIcketEmailHandler : IRequestHandler<NewTIcketEmailCommand, ErrorOr<Unit>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly NewTIcketEmailServices _emailServices;

    public NewTIcketEmailHandler(IUnitOfWork unitOfWork, NewTIcketEmailServices emailServices)
    {
        _emailServices = emailServices ?? throw new ArgumentNullException(nameof(emailServices));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(NewTIcketEmailCommand command, CancellationToken cancellationToken)
    {

        if (Correo.Create(command.recipientEmail) is not Correo internalcorreo)
        {
            return Errors.SendInternalCorreo.InternalCorreoWithBadFormat; //retorno el tipo de error que defini en la calse errors de dominio
        }

        await _emailServices.EnviarCorreoUser(
            command.recipientEmail,
            command.id,
            command.Nombre,
            command.Estado,
            command.FechaDeCreacion,
            command.Prioridad,
            command.CreadoPor
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}

