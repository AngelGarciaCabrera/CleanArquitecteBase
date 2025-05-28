using Domain.Tickets;
using MediatR;
using ErrorOr;
using Application.Tickets.GetAll;

namespace Application.Tickets.GetById
{
    internal sealed class GetTicketsByIdCommandHandler : IRequestHandler<GetTicketsByIdCommand, ErrorOr<TicketsResponse>>
    {
        private readonly ITicketsRepository _TicketsRepository;

        public GetTicketsByIdCommandHandler(ITicketsRepository TicketsRepository)
        {
            _TicketsRepository = TicketsRepository ?? throw new ArgumentNullException(nameof(TicketsRepository));
        }

        public async Task<ErrorOr<TicketsResponse>> Handle(GetTicketsByIdCommand command, CancellationToken cancellationToken)
        {
            var Tickets = await _TicketsRepository.GetByIdAsnyc(command.Id); // ? Corrección del typo en Async

            if (Tickets is null)
            {
                throw new KeyNotFoundException("La Tickets no existe.");
            }

            var response = new TicketsResponse(
                Tickets.Id,
                Tickets.Nombre,
                Tickets.Descripcion,
                Tickets.Estado,
                Tickets.Departamento_Id,
                Tickets.User_Id ,
                Tickets.FechaDeCreacion,
                Tickets.Prioridad,
                Tickets.CreadoPor
            );

            return response;
        }
    }
}
