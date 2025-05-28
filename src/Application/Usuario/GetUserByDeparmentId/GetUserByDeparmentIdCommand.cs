using Domain.Usuario;
using ErrorOr;
using MediatR;

namespace Application.Usuario.GetUserByDeparmentId
{
    public record GetUserByDeparmentIdCommand(int Departamento_Id) :  IRequest<ErrorOr<List<Domain.Usuario.Usuario>>>;
}
