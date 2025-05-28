
using ErrorOr;
using MediatR;


namespace Application.EnviarCorreo.NewTIcketEmail{

    public record NewTIcketEmailCommand(
        string recipientEmail,
        int id,
        string Nombre,
        string Estado,
        string FechaDeCreacion,
        string Prioridad,
        string CreadoPor


    ): IRequest<ErrorOr<Unit>>;
}

