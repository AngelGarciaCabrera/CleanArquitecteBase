using Application.Tickets.GetAll;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetTickesByCreator
{
    public sealed class GetTicketsByCreatorCommand : IRequest<ErrorOr<List<TicketsResponse>>>
    {
        public string UsuarioCorreo { get; }
        public int Page { get; }
        public int Limit { get; }

        public GetTicketsByCreatorCommand(string usuarioCorreo, int page, int limit)
        {
            UsuarioCorreo = usuarioCorreo;
            Page = page;
            Limit = limit;
        }
    }
}
