using ErrorOr;
using MediatR;

namespace Application.EnviarCorreo.InternalCorreo
{

    public record EnviarInternalCorreo(
        int CentroId,
        string correo,
        string Descripcion,
        string Fecha,
        string Seccion,
        string Telefono,
        string CentroNombre,
        string Usuario_Nombre


    ) : IRequest<ErrorOr<Unit>>;
}

