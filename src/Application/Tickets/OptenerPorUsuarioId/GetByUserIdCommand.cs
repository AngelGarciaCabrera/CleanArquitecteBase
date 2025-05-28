using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Usuario.OptenerPorUsuarioId
{
    public record GetByUserIdCommand(int User_Id) :  IRequest<ErrorOr<List<Domain.Tickets.Tickets>>>;
}
