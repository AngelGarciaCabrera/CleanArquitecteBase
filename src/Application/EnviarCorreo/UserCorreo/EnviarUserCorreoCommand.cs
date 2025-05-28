
using ErrorOr;
using MediatR;


namespace Application.EnviarCorreo.UserCorreo{

    public record EnviarUserCorreo(
        string recipientEmail,
        string userName

    ): IRequest<ErrorOr<Unit>>;
}

