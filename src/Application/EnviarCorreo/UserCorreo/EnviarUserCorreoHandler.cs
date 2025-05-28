using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;

namespace Application.EnviarCorreo.UserCorreo;
using Domain.DomainErros;


public sealed class EnviarUserCorreoHandler : IRequestHandler<EnviarUserCorreo, ErrorOr<Unit>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly Email_User_Service _emailServices;

    public EnviarUserCorreoHandler( IUnitOfWork unitOfWork , Email_User_Service emailServices)
    {
        _emailServices = emailServices ?? throw new ArgumentNullException(nameof(emailServices));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(EnviarUserCorreo command, CancellationToken cancellationToken)
    {

         if (Correo.Create(command.recipientEmail) is not Correo internalcorreo)
        {
            return Errors.SendInternalCorreo.InternalCorreoWithBadFormat; //retorno el tipo de error que defini en la calse errors de dominio
        }

        await _emailServices.EnviarCorreoUser(
            command.recipientEmail,
            command.userName
        );
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}

