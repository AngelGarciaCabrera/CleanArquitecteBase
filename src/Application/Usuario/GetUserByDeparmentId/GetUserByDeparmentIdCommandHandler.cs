// GetUserByDeparmentIdCommandHandler.cs
using ErrorOr;
using MediatR;
using Domain.Usuario;
using Domain.DomainErros;
using System.Collections.Generic;

namespace Application.Usuario.GetUserByDeparmentId
{
    public class GetUserByDeparmentIdCommandHandler : IRequestHandler<GetUserByDeparmentIdCommand, ErrorOr<List<Domain.Usuario.Usuario>>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUserByDeparmentIdCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ErrorOr<List<Domain.Usuario.Usuario>>> Handle(GetUserByDeparmentIdCommand command, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.GetByUsersByDeparmentIdAsync(command.Departamento_Id);

            if (usuarios == null || usuarios.Count == 0)
            {
                return Errors.Usuario.UsuarioNotFound;
            }

            return usuarios;
        }
    }
}
