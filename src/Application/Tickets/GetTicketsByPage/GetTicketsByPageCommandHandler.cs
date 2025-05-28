using Application.Tickets.GetAll;
using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetTicketsByPage
{
    internal sealed class GetTicketsByPageHandler : IRequestHandler<GetTicketsByPageCommand, ErrorOr<List<TicketsResponse>>>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public GetTicketsByPageHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<TicketsResponse>>> Handle(GetTicketsByPageCommand command, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetByPageAsync(command.Page, command.Limit);

            if (tickets == null || !tickets.Any())
                return Error.NotFound(description: "No hay más tickets disponibles");

            return tickets.Select(s => new TicketsResponse(
                s.Id,
                s.Nombre,
                s.Descripcion,
                s.Estado,
                s.Departamento_Id,
                s.User_Id,
                s.FechaDeCreacion,
                s.Prioridad,
                s.CreadoPor
            )).ToList();
        }
    }
}
