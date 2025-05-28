using Application.Tickets.Delete;
using FluentValidation;

namespace Application.Tickets.Delete;

public class DeleteTicketsCommandValidation : AbstractValidator<DeleteTicketsCommand>
{
    public DeleteTicketsCommandValidation()
    {
        RuleFor(v => v.Id)
        .NotEmpty();
    }
}
