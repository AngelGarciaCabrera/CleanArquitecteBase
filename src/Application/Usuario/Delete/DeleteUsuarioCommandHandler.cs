
using Domain.Usuario;
using Domain.Primitives;
using ErrorOr;
using MediatR;


namespace Application.Usuario.Delete
{
    internal sealed class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
            if (usuario is null)
            {
                return Error.NotFound("Usuario no encontrado", "El Usuario no existe");
            }

            await _usuarioRepository.DeleteAsync(usuario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }


    }
}