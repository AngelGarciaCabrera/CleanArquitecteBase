
using ErrorOr;
using MediatR;

namespace Application.Usuario.Create
{
    public record CreateUsuarioCommand(
        int Id,
        string Nombre,
        string Correo,
        string Posicion,
        string Contraseña,
        int Departamento_id,
        int Preferencias_id,
        string Rol): IRequest<ErrorOr<Unit>>;
}

