using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;

namespace Application.EnviarCorreo.InternalCorreo;
using Domain.DomainErros;


public sealed class EnviarInternalCorreoHandler : IRequestHandler<EnviarInternalCorreo, ErrorOr<Unit>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly Email_Interal_Service _emailServices;

    public EnviarInternalCorreoHandler(IUnitOfWork unitOfWork, Email_Interal_Service emailServices)
    {
        _emailServices = emailServices ?? throw new ArgumentNullException(nameof(emailServices));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(EnviarInternalCorreo command, CancellationToken cancellationToken)
    {

        if (Correo.Create(command.correo) is not Correo usercorreo)
        {
            return Errors.SendUserCorreo.UserCorreoWithBadFormat; //retorno el tipo de error que defini en la calse errors de dominio
        }
        if (PhoneNumber.Create(command.Telefono) is not PhoneNumber internalcorreo)
        {
            return Errors.Phone.PhoneNumberWithBadFormat; //retorno el tipo de error que defini en la calse errors de dominio
        }

        int centroId;

        if(command.CentroId is int id){
            centroId = id;
        }
        else if (!int.TryParse(command.CentroId.ToString(), out centroId))
        {
            return Error.Validation("Sugerencia.CentroId", "El ID del centro debe ser un número entero válido");
        }

        await _emailServices.EnviarCorreoInternal(
            command.correo,
            command.Usuario_Nombre,
            centroId, 
            command.Descripcion,
            command.Fecha,
            command.Seccion,
            command.Telefono,
            command.CentroNombre

        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}

