using Domain.Usuario;
using MediatR;
using ErrorOr;
using Application.Usuario.GetAll;
using Application.Usuario.GetById;

namespace Application.Centro.GetById
{
    internal sealed class GetUsuarioByIdCommandHandler : IRequestHandler<GetUsuarioByIdCommand, ErrorOr<UsuarioResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioByIdCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<ErrorOr<UsuarioResponse>> Handle(GetUsuarioByIdCommand command, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(command.Id);

            if (usuario is null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            var response = new UsuarioResponse(
                usuario.Id,
                usuario.Nombre,
                usuario.Correo?.Value ?? string.Empty,
                usuario.Posicion,
                usuario.Contraseña,
                usuario.Departamento_id,
                usuario.Preferencias_id,
                usuario.Rol?.Value ?? string.Empty
            );

            return response;

        }
    }
}
