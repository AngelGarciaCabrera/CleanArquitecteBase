using Domain.Primitives;
using Domain.Tickets;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Tickets.Create;

internal sealed class CreateTicketsCommandHandler : IRequestHandler<CreateTicketsCommand, ErrorOr<int>>
{
    private readonly ITicketsRepository _TicketsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketsCommandHandler(ITicketsRepository TicketsRepository, IUnitOfWork unitOfWork)
    {
        _TicketsRepository = TicketsRepository ?? throw new ArgumentNullException(nameof(TicketsRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<int>> Handle(CreateTicketsCommand command, CancellationToken cancellationToken)
    {
        var correo = Correo.Create(command.CreadoPor);

        try
        {
            var Tickets = Domain.Tickets.Tickets.Create(
                1, // Puedes reemplazar esto si tu lógica de generación de ID es distinta
                command.Nombre,
                command.Descripcion,
                command.Estado,
                command.Departamento_Id,
                command.User_Id,
                command.FechaDeCreacion,
                command.Prioridad,
                correo
            );

            await _TicketsRepository.AddAsync(Tickets);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Tickets.Id; // ?? Devuelve el ID generado al frontend
        }
        catch (ArgumentException ex)
        {
            return Error.Validation("Tickets", ex.Message);
        }
    }
}
