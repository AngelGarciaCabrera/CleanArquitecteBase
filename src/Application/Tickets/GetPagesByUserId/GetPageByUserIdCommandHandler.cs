using Application.Tickets.GetAll;
using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetByDepartamentoYPagina
{
    internal sealed class GetPageByUserIdCommandHandler
        : IRequestHandler<GetPageByUserIdCommand, ErrorOr<List<TicketsResponse>>>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public GetPageByUserIdCommandHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<TicketsResponse>>> Handle(GetPageByUserIdCommand request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetPagesByUserId(request.User_Id, request.Page, request.Limit);

            var mapped = tickets?.Select(t => new TicketsResponse(
                t.Id,
                t.Nombre,
                t.Descripcion,
                t.Estado,
                t.Departamento_Id,
                t.User_Id,
                t.FechaDeCreacion,
                t.Prioridad,
                t.CreadoPor
            )).ToList() ?? new List<TicketsResponse>();

            return mapped; // simplemente devuelve la lista vacía
        }

    }
}
