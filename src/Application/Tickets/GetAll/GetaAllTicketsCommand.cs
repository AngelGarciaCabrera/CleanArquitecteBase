using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Tickets.GetAll
{
    public record GetAllTicketsCommand() : IRequest<ErrorOr<List<TicketsResponse>>>;

    public record TicketsResponse(
        int Id,
        string Nombre,
        string Descripcion,
        string Estado,
        int Departamento_Id,
        int User_Id,
        DateTime FechaDeCreacion,
        string Prioridad,
        Correo CreadoPor
    );
}
