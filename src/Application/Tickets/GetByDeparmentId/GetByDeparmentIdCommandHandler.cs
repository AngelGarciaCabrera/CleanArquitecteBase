// GetUserByDeparmentIdCommandHandler.cs
using ErrorOr;
using MediatR;
using Domain.Tickets;
using Domain.DomainErros;
using System.Collections.Generic;
using Application.Usuario.GetByDeparmentId;

namespace Application.Tickets.GetByDeparmentId
{
    public class GetByDeparmentIdCommandHandler : IRequestHandler<GetByDeparmentIdCommand, ErrorOr<List<Domain.Tickets.Tickets>>>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public GetByDeparmentIdCommandHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ErrorOr<List<Domain.Tickets.Tickets>>> Handle(GetByDeparmentIdCommand command, CancellationToken cancellationToken)
        {
            var tickets = await _ticketsRepository.GetByDeparmentIdAsync(command.Departamento_Id);

            if (tickets == null || tickets.Count == 0)
            {
                return Errors.Tickets.TicketsByDeparmentNotFound;
            }

            return tickets;
        }
    }
}
