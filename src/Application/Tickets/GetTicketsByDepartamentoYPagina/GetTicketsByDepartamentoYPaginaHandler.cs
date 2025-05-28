using Application.Tickets.GetAll;
using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetByDepartamentoYPagina
{
    internal sealed class GetTicketsByDepartamentoYPaginaHandler 
        : IRequestHandler<GetTicketsByDepartamentoYPaginaCommand, ErrorOr<List<TicketsResponse>>>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public GetTicketsByDepartamentoYPaginaHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<TicketsResponse>>> Handle(GetTicketsByDepartamentoYPaginaCommand request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetByDepartamentoAndPageAsync(request.Departamento_Id, request.Page, request.Limit);

            if (tickets == null || !tickets.Any())
                return Error.NotFound(description: "No hay tickets disponibles para este departamento en esta página.");

            return tickets.Select(t => new TicketsResponse(
                t.Id,
                t.Nombre,
                t.Descripcion,
                t.Estado,
                t.Departamento_Id,
                t.User_Id,
                t.FechaDeCreacion,
                t.Prioridad,
                t.CreadoPor
            )).ToList();
        }
    }
}
