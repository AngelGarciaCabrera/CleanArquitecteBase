using MediatR;
using ErrorOr;
using Application.Tickets.GetAll;

namespace Application.Tickets.GetTicketsByPage
{
    public sealed class GetTicketsByPageCommand : IRequest<ErrorOr<List<TicketsResponse>>>
    {
        public int Page { get; }
        public int Limit { get; }

        public GetTicketsByPageCommand(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
    }
}
