
using Application.Tickets.GetAll;
using Application.Tickets.GetAll;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetById

{

    public sealed class GetTicketsByIdCommand : IRequest<ErrorOr<TicketsResponse>>

    {
        public int Id { get; }

        public GetTicketsByIdCommand(int id)

        {
            Id = id;
        }

    }

}
