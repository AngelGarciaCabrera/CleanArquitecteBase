using ErrorOr;
using MediatR;
using System.Collections.Generic;

namespace Application.Usuario.GetAll
{
    public record GetAllUsuarioCommand() : IRequest<ErrorOr<List<UsuarioResponse>>>;
    

    public record UsuarioResponse(
        int Id,
        string Nombre,
        string Correo,
        string Posicion,
        string Contraseña,
        int Departamento_id,
        int Preferencias_id,
        string Rol
    );
}
