using Application.Usuario.Update;
using FluentValidation;

namespace Application.Centro.Update;

public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
{
    public UpdateUsuarioCommandValidator()
    {
        RuleFor(v => v.Nombre)
            .MaximumLength(255)
            .NotEmpty();
        RuleFor(v => v.Correo)
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
           RuleFor(v => v.Rol)
            .MaximumLength(50)
            .NotEmpty();
    }
}