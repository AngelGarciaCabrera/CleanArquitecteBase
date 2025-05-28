using Domain.Usuario;
using MediatR;
using ErrorOr;
using Application.Usuario.GetAll;


namespace Application.Centro.GetAll
{
    internal sealed class GetAllUsuarioCommandHandler : IRequestHandler<GetAllUsuarioCommand, ErrorOr<List<UsuarioResponse>>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetAllUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<ErrorOr<List<UsuarioResponse>>> Handle(GetAllUsuarioCommand command, CancellationToken cancellationToken)
        {

            var usuario = await _usuarioRepository.GetAllAsync();
            
            var usuarioResponses = usuario.Select(usario => new UsuarioResponse(
                            usario.Id,
                            usario.Nombre,
                            usario.Correo?.Value ?? string.Empty,
                            usario.Posicion,
                            usario.Contraseña,
                            usario.Departamento_id,
                            usario.Preferencias_id,
                            usario.Rol?.Value ?? string.Empty
                        )).ToList();
                        
            return usuarioResponses;
        }
    }
}
