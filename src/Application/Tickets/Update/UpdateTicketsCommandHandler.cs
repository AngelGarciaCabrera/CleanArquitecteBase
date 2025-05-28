using Domain.Tickets;
using Domain.Primitives;
using MediatR;
using ErrorOr;
using Domain.ValueObjects;

namespace Application.Tickets.Update
{
    internal sealed class UpdateTicketsCommandHandler : IRequestHandler<UpdateTicketsCommand, ErrorOr<Unit>>
    {
        private readonly ITicketsRepository _TicketsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTicketsCommandHandler(ITicketsRepository TicketsRepository, IUnitOfWork unitOfWork)
        {
            _TicketsRepository = TicketsRepository ?? throw new ArgumentNullException(nameof(TicketsRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateTicketsCommand command, CancellationToken cancellationToken)
        {

            //var Tickets = await _TicketsRepository.GetByIdAsnyc(new TicketsId(Guid.Parse(command.Id)));

            var Tickets = await _TicketsRepository.GetByIdAsnyc(command.Id);
            var correo = Correo.Create(command.CreadoPor);

            if (Tickets is null)
            {
                return Error.NotFound("Tickets.NoEncontrado", "El Tickets no existe");
            }

            Tickets.Update( command.Id,
                command.Nombre,
                command.Descripcion,
                command.Estado,
                command.Departamento_Id,
                command.FechaDeCreacion,
                command.User_Id,
                command.Prioridad,
                correo
            );

            await _TicketsRepository.UpdateAsync(Tickets);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
