using Application.Tickets.GetAll;
using Application.Tickets.GetByDepartamentoYPagina;
using Application.Tickets.GetTickesByCreator;
using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetTickesByCreator
{
    internal sealed class GetTicketsByCreatorCommandHandler
        : IRequestHandler<GetTicketsByCreatorCommand, ErrorOr<List<TicketsResponse>>>
    {
        private readonly ITicketsRepository _ticketsRepository;
        
        public GetTicketsByCreatorCommandHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<TicketsResponse>>> Handle(GetTicketsByCreatorCommand request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetPagesByUserCreator(request.UsuarioCorreo, request.Page, request.Limit);

            if (tickets == null || !tickets.Any())
                return Error.NotFound(description: "No hay tickets disponibles creado por este usuario en esta página.");

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
