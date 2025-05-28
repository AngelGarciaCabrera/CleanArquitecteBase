using FluentValidation;

namespace Application.Usuario.Delete;

public class DeleteUsuarioCommandValidator : AbstractValidator<DeleteUsuarioCommand>
{
    public DeleteUsuarioCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}