using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Tickets.Update
{
    public record UpdateTicketsCommand(
            int Id,
            string Nombre,
            string Descripcion,
            string Estado, 
            int Departamento_Id,
            int User_Id,
            DateTime FechaDeCreacion,
            string Prioridad,
            string CreadoPor
    ) : IRequest<ErrorOr<Unit>>;
}
