using Domain.Tickets;
using MediatR;
using ErrorOr;
using Application.Tickets.GetAll;


namespace Application.Tickets.GetAll
{
    internal sealed class GetAllTicketsCommandHandler : IRequestHandler<GetAllTicketsCommand, ErrorOr<List<TicketsResponse>>>
    {
        private readonly ITicketsRepository _TicketsRepository;

        public GetAllTicketsCommandHandler(ITicketsRepository TicketsRepository)
        {
            _TicketsRepository = TicketsRepository ?? throw new ArgumentNullException(nameof(TicketsRepository));
        }

        public async Task<ErrorOr<List<TicketsResponse>>> Handle(GetAllTicketsCommand command, CancellationToken cancellationToken)
        {

            var Tickets = await _TicketsRepository.GetAllAsync();

            var TicketsResponses = Tickets.Select(Tickets => new TicketsResponse(
                Tickets.Id,
                Tickets.Nombre,
                Tickets.Descripcion,
                Tickets.Estado,
                Tickets.Departamento_Id ,
                Tickets.User_Id ,
                Tickets.FechaDeCreacion,
                Tickets.Prioridad,
                Tickets.CreadoPor
              
              
            )).ToList();

            return TicketsResponses;
        }
    }
}
