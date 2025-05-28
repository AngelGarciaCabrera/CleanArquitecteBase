

using ErrorOr;
using MediatR;

namespace Application.Usuario.Delete
{
    public  record DeleteUsuarioCommand(int Id):IRequest<ErrorOr<Unit>>;
    
}