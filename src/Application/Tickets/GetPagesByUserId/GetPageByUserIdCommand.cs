using Application.Tickets.GetAll;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetByDepartamentoYPagina
{
    public sealed class GetPageByUserIdCommand : IRequest<ErrorOr<List<TicketsResponse>>>
    {
        public int User_Id { get; }
        public int Page { get; }
        public int Limit { get; }

        public GetPageByUserIdCommand(int user_Id, int page, int limit)
        {
            User_Id = user_Id;
            Page = page;
            Limit = limit;
        }
    }
}
