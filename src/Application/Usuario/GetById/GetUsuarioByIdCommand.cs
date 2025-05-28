
using Application.Centro.GetAll;
using Application.Usuario.GetAll;
using ErrorOr;
using MediatR;

namespace Application.Usuario.GetById

{

    public sealed class GetUsuarioByIdCommand : IRequest<ErrorOr<UsuarioResponse>>

    {

        public int Id { get; }


        public GetUsuarioByIdCommand(int id)
        {

            Id = id;
        }

    }

}
