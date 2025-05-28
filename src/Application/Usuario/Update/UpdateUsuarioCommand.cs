using ErrorOr;
using MediatR;

namespace Application.Usuario.Update
{
    public record UpdateUsuarioCommand(
        int Id,
        string Nombre,
        string Correo,
        string Posicion,
        string Contraseña,
        int Departamento_id,
        int Preferencias_id,
        string Rol
    ) : IRequest<ErrorOr<Unit>>;
}
