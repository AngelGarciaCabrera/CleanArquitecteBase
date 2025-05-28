using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using System.Text.Json.Serialization;
namespace Application.Tickets.Create;


public record CreateTicketsCommand(
     int Id,
    string Nombre,
    string Descripcion,
    [property: JsonPropertyName("departamento_Id")] int Departamento_Id,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("user_Id")] int User_Id,
    [property: JsonPropertyName("fechaDeCreacion")] DateTime FechaDeCreacion,
     string Prioridad,
     string CreadoPor
    ): IRequest<ErrorOr<int>>;


