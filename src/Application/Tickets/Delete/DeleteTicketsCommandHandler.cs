

using Domain.Primitives;
using Domain.Tickets;
using ErrorOr;
using MediatR;


namespace Application.Tickets.Delete
{
    internal sealed class DeleteTicketsCommandHandler : IRequestHandler<DeleteTicketsCommand, ErrorOr<Unit>>
    {
        private readonly ITicketsRepository _TicketsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTicketsCommandHandler(ITicketsRepository TicketsRepository, IUnitOfWork unitOfWork)
        {
            _TicketsRepository = TicketsRepository ?? throw new ArgumentNullException(nameof(TicketsRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteTicketsCommand request, CancellationToken cancellationToken)
        {

            //var sugerencia = await _sugerenciaRepository.GetByIdAsnyc(new SugerenciaId(Guid.Parse(request.Id)));

            var Tickets = await _TicketsRepository.GetByIdAsnyc((request.Id));
            if (Tickets is null)
            {
                return Error.NotFound("Tickets no encontrado", "el Tickets no existe");
            }

            await _TicketsRepository.DeleteAsync(Tickets);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }

    }
}