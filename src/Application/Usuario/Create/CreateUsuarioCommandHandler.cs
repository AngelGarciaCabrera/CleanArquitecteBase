using Domain.Primitives;
using Domain.Usuario;
using Domain.ValueObjects;
using MediatR;
using ErrorOr;
namespace Application.Usuario.Create;
using Domain.DomainErros;
using BCrypt.Net; //encriptador de la contraseña
using DocumentFormat.OpenXml.Office2010.CustomUI;

public sealed class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, ErrorOr<Unit>>
{
    private readonly IUsuarioRepository _UsuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _UsuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken)
    {

        if (Correo.Create(command.Correo) is not Correo correo)
        {
            return Errors.Correo.CorreoWithBadFormat; //retorno el tipo de error que defini en la calse errors de dominio
        }
        if (!Rol.TryCreate(command.Rol, out var rol))
        {
            return Errors.Rol.RolNoIsValid;
        }


        // ?? Hashear la contraseña
        string hashedPassword = BCrypt.HashPassword(command.Contraseña);

        var usuario = new Domain.Usuario.Usuario(0,
            command.Nombre,
            correo,
            command.Posicion,
            hashedPassword,
            command.Departamento_id,
            command.Preferencias_id,
            rol
        );

        await _UsuarioRepository.AddAsync(usuario);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

}

