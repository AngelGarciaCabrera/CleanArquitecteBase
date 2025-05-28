

using Domain.Tickets;
using ErrorOr;
using MediatR;

namespace Application.Usuario.GetByDeparmentId
{
    public record GetByDeparmentIdCommand(int Departamento_Id) :  IRequest<ErrorOr<List<Domain.Tickets.Tickets>>>;
}
