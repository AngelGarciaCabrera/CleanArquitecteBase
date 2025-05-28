using FluentValidation;

namespace Application.Usuario.Create;

public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
{
    public CreateUsuarioCommandValidator()
    {
        RuleFor(v => v.Nombre)
            .MaximumLength(255)
            .NotEmpty();
        RuleFor(v => v.Correo)
            .MaximumLength(255)
            .MinimumLength(10)
            .NotEmpty();
        RuleFor(v => v.Posicion)
            .MaximumLength(200)
            .MinimumLength(5)
            .NotEmpty();
        RuleFor(v => v.Contraseña)
            .MaximumLength(200)
            .MinimumLength(8)
            .NotEmpty();
        RuleFor(v => v.Departamento_id)
            .NotEmpty();
        RuleFor(v => v.Preferencias_id)
            .NotEmpty();
        RuleFor(V=> V.Rol)
            .NotEmpty()
            .MaximumLength(50);
    }
}