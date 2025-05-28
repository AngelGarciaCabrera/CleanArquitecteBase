

using ErrorOr;
using MediatR;

namespace Application.Tickets.Delete
{
    public  record DeleteTicketsCommand(int Id):IRequest<ErrorOr<Unit>>;
    
}