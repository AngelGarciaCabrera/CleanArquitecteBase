using ErrorOr;
using MediatR;
using Domain.Usuario;
using BCrypt.Net;
using Domain.DomainErros;

namespace Application.Usuario.Login
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, ErrorOr<Domain.Usuario.Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ErrorOr<Domain.Usuario.Usuario>> Handle(LoginUsuarioCommand command, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByCorreoAsync(command.Correo);
            if (usuario is null)
            {
                return Errors.Usuario.UsuarioNotFound;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(command.Contraseña, usuario.Contraseña);
            if (!isPasswordValid)
            {
                return Errors.Usuario.InvalidPassword;
            }

            return usuario;
        }
    }
}
