// GetUserByDeparmentIdCommandHandler.cs
using ErrorOr;
using MediatR;
using Domain.Tickets;
using Domain.DomainErros;
using System.Collections.Generic;

namespace Application.Usuario.OptenerPorUsuarioId
{
    public class GetUserByDeparmentIdCommandHandler : IRequestHandler<GetByUserIdCommand, ErrorOr<List<Domain.Tickets.Tickets>>>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public GetUserByDeparmentIdCommandHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<Domain.Tickets.Tickets>>> Handle(GetByUserIdCommand command, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetByUserIdAsync(command.User_Id);

            // Si el usuario existe pero no tiene tickets, devolvemos una lista vacía
            return tickets ?? new List<Domain.Tickets.Tickets>();
        }
    }
}
