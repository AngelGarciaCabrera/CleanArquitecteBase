using Domain.Usuario;
using ErrorOr;
using MediatR;

namespace Application.Usuario.Login
{
    public record LoginUsuarioCommand(string Correo, string Contraseña) : IRequest<ErrorOr<Domain.Usuario.Usuario>>;
}
