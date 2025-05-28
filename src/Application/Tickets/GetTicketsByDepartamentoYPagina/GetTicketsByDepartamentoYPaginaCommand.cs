using Application.Tickets.GetAll;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetByDepartamentoYPagina
{
    public sealed class GetTicketsByDepartamentoYPaginaCommand : IRequest<ErrorOr<List<TicketsResponse>>>
    {
        public int Departamento_Id { get; }
        public int Page { get; }
        public int Limit { get; }

        public GetTicketsByDepartamentoYPaginaCommand(int departamentoId, int page, int limit)
        {
            Departamento_Id = departamentoId;
            Page = page;
            Limit = limit;
        }
    }
}
